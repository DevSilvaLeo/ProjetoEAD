using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Servicos.Models
{
    public class UsuarioAuthenticate
    {
        [Required(ErrorMessage = "Informe o nome do usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o email do usuário")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Informe a senha do usuário")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        [Required(ErrorMessage = "Confirme a senha do usuário")]
        public string SenhaConfirm { get; set; }

        [Required(ErrorMessage = "Informe um apelido para o usuário")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "Informe um tipo para o usuário")]
        public string Tipo { get; set; }
    }
}
