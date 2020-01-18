using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESC.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Usuário", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe O Documento para Identificação", AllowEmptyStrings = false)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe a Senha", AllowEmptyStrings = false)]
        [StringLength(6, MinimumLength = 4)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o Login", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 14, ErrorMessage = "Informe o Login de acesso")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Confirme a Senha", AllowEmptyStrings = false)]
        [StringLength(6, MinimumLength = 4)]
        public string ConfirmarSenha { get; set; }

        public Empresa Empresa { get; set; }

        [Required(ErrorMessage = "Informe o Nome da Empresa", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Informe o Nome da Empresa")]
        [Display(Name = "Nome da Empresa")]
        public string NomeEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ da Empresa", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 14, ErrorMessage = "Informe o CNPJ da Empresa")]
        [Display(Name = "CNPJ da Empresa")]
        public string CNPJEmpresa { get; set; }

        public string AutorizadorPor { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAutorizacao { get; set; }

        public bool Ativo { get; set; }

        public bool Administrador { get; set; }
    }
}