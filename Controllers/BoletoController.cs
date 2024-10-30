using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBoleto.Data;
using SistemaBoleto.Models;

namespace SistemaBoleto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BoletoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Boleto>> PostBoleto(Boleto boleto)
        {
            // Validar o modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Adicionar o boleto ao contexto
            _context.Boletos.Add(boleto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = boleto.Id }, boleto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Boleto>> GetById(int id)
        {
            var boleto = await _context.Boletos
            .Include(b => b.Banco)  // Inclui o banco associado ao boleto
            .FirstOrDefaultAsync(b => b.Id == id);

            if (boleto == null)
            {
                return NotFound(new { message = "Boleto não encontrado" });
            }

            if (DateTime.Now > boleto.DataVencimento)
            {
                var juros = boleto.Banco.PercentualJuros / 100; // Converte percentual para decimal
                var valorComJuros = boleto.Valor + (boleto.Valor * juros);
                
                // Inclui o valor com juros no retorno
                return Ok(new
                {
                    boleto.Id,
                    boleto.NomePagador,
                    boleto.CPFCNPJPagador,
                    boleto.NomeBeneficiario,
                    boleto.CPFCNPJBeneficiario,
                    ValorOriginal = boleto.Valor,
                    ValorComJuros = valorComJuros,
                    DataVencimento = boleto.DataVencimento,
                    Status = "Vencido",
                    Observacao = boleto.Observacao,
                    BancoId = boleto.BancoId,
                    NomeBanco = boleto.Banco.Nome
                });

            }
            return Ok(boleto);
            
        }
         // Verifica se o boleto está vencido
    }
}
