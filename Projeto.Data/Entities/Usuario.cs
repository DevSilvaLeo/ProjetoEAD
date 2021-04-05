using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Apelido { get; set; }
        public string Tipo { get; set; }

    }
}
