using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o Usuário")]
        [Display(Name = "Usurário")]
        public string Usuario { get; set; }

        [Display(Name = "Lembrar Me")]
        public bool LembrarMe { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}