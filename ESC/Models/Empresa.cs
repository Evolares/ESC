using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESC.Models
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "Tamanho máximo são 100 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Nome Fantasia é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Nome Fantasia")]
        [StringLength(100, ErrorMessage = "Tamanho máximo são 100 caracteres!")]
        public string NomeFantasia { get; set; }

        [DisplayName("CNPJ")]
        [Required(ErrorMessage = "O CNPJ é obrigatório", AllowEmptyStrings = false)]
        [StringLength(18, ErrorMessage = "Tamanho máximo são 18 caracteres!")] 
        public string CNPJ { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O Endereço é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "Tamanho máximo são 200 caracteres!")]
        public string Endereco { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "A Cidade é obrigatória", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Tamanho máximo são 100 caracteres!")]
        public string Cidade { get; set; }

        [DisplayName("UF")]
        [Required(ErrorMessage = "UF", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Tamanho máximo são 2 caracteres!")]
        public string UF { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório", AllowEmptyStrings = false)]
        [StringLength(9, ErrorMessage = "Tamanho máximo são 9 caracteres!")]
        public string CEP { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O Telefone é Obrigatório", AllowEmptyStrings = false)]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Tamanho máximo são 13 caracteres!")]
        public string Telefone { get; set; }

        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage ="O Email deve ter no mínimo 5 e no máximo 100 caracteres.")]
        public string email { get; set; }

        [DisplayName("Capital Social")]
        [Required(ErrorMessage = "O Capital Social é obrigatório", AllowEmptyStrings = false)]
        public decimal CapitalSocial { get; set; }

        [DisplayName("Ativa")]
        public bool Ativa { get; set; }
    }
}