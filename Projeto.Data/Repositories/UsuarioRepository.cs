using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dapper;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Projeto.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string connectionString;

        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Inserir(Usuario obj)
        {
            var query = "Insert Into Usuario(Nome, Email, Senha, Apelido, Tipo) values (@Nome, @Email, @Senha, @Apelido, @Tipo)";

           // using(var conn = new MySqlConnection(connectionString))
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);
            }
        }

        public void Editar(Usuario obj)
        {
            var query = "update Usuario set Nome = @Nome, Email = @Email, Senha = @Senha, Apelido = @Apelido, Tipo = @Tipo where IdUsuario = @IdUsuario";
            //using (var conn = new MySqlConnection(connectionString))
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);
            }
        }

        public void Excluir(int id)
        {
            var query = "delete frm Usuario where IdUsuario = @IdUsuario";
            //using (var conn = new MySqlConnection(connectionString))
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, new { IdUsuario = id });
            }
        }

        public List<Usuario> ConsultarTodos()
        {
            var query = "select * from Usuario";
            //using (var conn = new MySqlConnection(connectionString))
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Usuario>(query).ToList();
            }
        }
        public Usuario ObterPorId(int id)
        {
            var query = "select * from Usuario where IdUsuario = @IdUsuario";
            // using (var conn = new MySqlConnection(connectionString))
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Usuario>(query, new { IdUsuario = id }).FirstOrDefault();
            }
        }

        public Usuario ObterEmail(string email)
        {
            var query = "select * from Usuario where Email = @Email";
            //using (var conn = new MySqlConnection(connectionString))
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Usuario>(query, new { Email = email }).FirstOrDefault();
            }

        }


        public Usuario ObterUsuario(string email, string senha)
        {
            var query = "select * from Usuario where Email = @Email and Senha = @Senha";
            //using (var conn = new MySqlConnection(connectionString))
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Usuario>(query, new { Email = email, Senha = senha }).FirstOrDefault();
            }
        }
    }
}
