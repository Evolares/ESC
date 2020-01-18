using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESC.Models
{
    public class EmpresaDadoBancario
    {
        public Empresa Empresa { get; set; }

        public int IdDadoBancario { get; set; }

        [Required(ErrorMessage = "O Número do Banco é obrigatório", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Tamanho máximo são 50 caracteres!")]
        [DisplayName("Núm do Banco")]
        public string NumBanco { get; set; }

        [Required(ErrorMessage = "O Nome do Banco é obrigatório", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Tamanho máximo são 50 caracteres!")]
        [DisplayName("Banco")]
        public string NomeBanco { get; set; }

        [Required(ErrorMessage = "A Agência do Banco é obrigatória", AllowEmptyStrings = false)]
        [StringLength(10, ErrorMessage = "Tamanho máximo são 10 caracteres!")]
        [DisplayName("Agência")]
        public string NumAgencia { get; set; }

        [Required(ErrorMessage = "O Número da Conta é obrigatória", AllowEmptyStrings = false)]
        [StringLength(10, ErrorMessage = "Tamanho máximo são 10 caracteres!")]
        [DisplayName("Núm Conta")]
        public string NumConta { get; set; }

        public TipoConta TipoConta { get; set; }

        [DisplayName("Ativa")]
        public bool Ativo { get; set; }

    }
}