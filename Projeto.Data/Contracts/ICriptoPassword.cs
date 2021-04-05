using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
    public interface ICriptoPassword
    {
        string Encrypt(string value);
    }
}
