using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESC.Models
{
    public class TipoConta
    {
        public int IdTipoConta { get; set; }

        [Required(ErrorMessage = "Tipo da Conta", AllowEmptyStrings = false)]
        [DisplayName("Tipo da Conta")]
        [StringLength(50, ErrorMessage = "Tamanho máximo são 50 caracteres!")]
        public string DescTipoConta { get; set; }
    }
}