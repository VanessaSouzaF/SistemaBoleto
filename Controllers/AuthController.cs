using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaBoleto.Data;
using SistemaBoleto.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaBoleto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public AuthController(AppDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] ClienteDto clienteDto)
        {
            // Verifique se o CPF/CNPJ já existe
            if (_context.Clientes.Any(c => c.CPF_CNPJ == clienteDto.CPF_CNPJ))
            {
                return BadRequest("CPF ou CNPJ já cadastrado.");
            }

            // Crie o hash da senha
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(clienteDto.Password);

            // Mapeie ClienteDto para Cliente
            var cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.Password = hashedPassword;

            // Crie o novo cliente
            // var cliente = new Cliente
            // {
            //     Nome = clienteDto.Nome,
            //     CPF_CNPJ = clienteDto.CPF_CNPJ,
            //     Password = hashedPassword,
            //     Endereco = clienteDto.Endereco
            // };

            // Salve o cliente no banco de dados
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok("Cliente cadastrado com sucesso.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // Verifique se o usuário existe
            var cliente = _context.Clientes.SingleOrDefault(c => c.CPF_CNPJ == loginDto.CPF_CNPJ);

            // Verifique as credenciais
            if (cliente == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, cliente.Password))
            {
                return Unauthorized("Credenciais inválidas."); // Retorna 401 se as credenciais forem inválidas
            }

            var token = GenerateJwtToken(cliente.ClienteId.ToString()); // Gere o token
            return Ok(new { token });
        }

        private string GenerateJwtToken(String userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
