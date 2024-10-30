using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaBoleto.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        
        [Required]
        public string? Nome { get; set; }
        
        [Required]
        public string? CPF_CNPJ { get; set; }
        
        public string? Endereco { get; set; }

        public string? Password { get; set; }

        
        public ICollection<Boleto>? Boletos { get; set; }
    }
}
