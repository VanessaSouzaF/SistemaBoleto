using System.ComponentModel.DataAnnotations;

namespace SistemaBoleto.Models
{
    public class Banco
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Nome do Banco é obrigatório.")]
        public string? Nome { get; set; } 

        [Required(ErrorMessage = "Código do Banco é obrigatório.")]
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "Percentual de Juros é obrigatório.")]
        public decimal PercentualJuros { get; set; } 
        public ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
    }
}
