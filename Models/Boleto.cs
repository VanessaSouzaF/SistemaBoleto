using System.ComponentModel.DataAnnotations;

namespace SistemaBoleto.Models
{
    public class Boleto
    {
        [Key]
        public int Id { get; set; } // Id (Obrigat�rio)

        [Required(ErrorMessage = "Nome do Pagador é obrigatório.")]
        public string? NomePagador { get; set; } // Nome do Pagador (Obrigat�rio)

        [Required(ErrorMessage = "CPF/CNPJ do Pagador é obrigatório.")]
        public string? CPFCNPJPagador { get; set; } // CPF/CNPJ do Pagador (Obrigat�rio)

        [Required(ErrorMessage = "Nome do Beneficiário é obrigatório.")]
        public string? NomeBeneficiario { get; set; } // Nome do Benefici�rio (Obrigat�rio)

        [Required(ErrorMessage = "CPF/CNPJ do Beneficiário é obrigatório.")]
        public string? CPFCNPJBeneficiario { get; set; } // CPF/CNPJ do Benefici�rio (Obrigat�rio)

        [Required(ErrorMessage = "Valor é obrigatório.")]
        public decimal Valor { get; set; } // Valor (Obrigat�rio)

        [Required(ErrorMessage = "Data de Vencimento é obrigatória.")]
        public DateTime DataVencimento { get; set; } // Data de Vencimento (Obrigat�rio)

        public string? Observacao { get; set; } // Observa��o (Opcional)

        [Required(ErrorMessage = "BancoId é obrigatório.")]
        public int BancoId { get; set; } 
        public Banco Banco { get; set; } = null!; 
    }
}

