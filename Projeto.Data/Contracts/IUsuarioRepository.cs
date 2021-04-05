using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario ObterEmail(string email);
        Usuario ObterUsuario(string email, string senha);
    }
}
