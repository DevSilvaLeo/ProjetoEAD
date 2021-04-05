using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Servicos.Models
{
    public class UsuarioAutheticateModel
    {
        [Required(ErrorMessage = "Informe o email do usuário")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário")]
        public string Senha { get; set; }

    }
}
