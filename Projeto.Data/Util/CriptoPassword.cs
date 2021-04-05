using System;
using System.Collections.Generic;
using System.Text;
using Projeto.Data.Contracts;
using System.Security.Cryptography;

namespace Projeto.Data.Util
{
    public class CriptoPassword : ICriptoPassword
    {
        public string Encrypt(string value)
        {
            var md5 = new MD5CryptoServiceProvider();

            var senha = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            var result = string.Empty;
            foreach (var item in senha)
            {
                result += item.ToString("X2");
            }

            return result;
        }
    }
}
