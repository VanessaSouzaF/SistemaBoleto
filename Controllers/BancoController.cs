using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaBoleto.Data;
using SistemaBoleto.Models;

namespace SistemaBoleto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BancoController(AppDbContext context)
        {
            _context = context;
        }

        // Ação POST para cadastrar um novo banco
        [HttpPost]
        public async Task<ActionResult<Banco>> PostBanco(Banco banco)
        {
            // Validar o modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Adicionar o banco ao contexto
            _context.Bancos.Add(banco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBanco), new { id = banco.Id }, banco);
        }

        // Ação GET para obter um banco específico (para o retorno do CreatedAtAction)
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Banco>> GetBanco(int id)
        {
            var banco = await _context.Bancos.FindAsync(id);

            if (banco == null)
            {
                return NotFound();
            }

            return banco;
        }

    }
}
