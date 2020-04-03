﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o seu Usúario")]
        [Display(Name ="Usúario:")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Informe a sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }



        [Display(Name = "Lembrar me")]
        public bool LembrarMe { get; set; }
    }
}