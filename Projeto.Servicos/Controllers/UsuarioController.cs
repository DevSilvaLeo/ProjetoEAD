using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Servicos.Models;
using Projeto.Servicos.Services;
using Projeto.Data.Util;

namespace Projeto.Servicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Post(UsuarioAuthenticate model, [FromServices] IUsuarioRepository rep)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(rep.ObterEmail(model.Email) != null)
                    {
                        return BadRequest("O email informado já está cadastrado no sistema");
                    }
                    else
                    {
                        Usuario u = new Usuario();
                        CriptoPassword cripto = new CriptoPassword();
                        u.Nome = model.Nome;
                        u.Email = model.Email;
                        u.Senha = cripto.Encrypt(model.Senha);
                        u.Apelido = model.Apelido;
                        u.Tipo = model.Tipo;

                        rep.Inserir(u);
                        return Ok("Usuário cadastrado com sucesso");

                    }                   
                }
                catch(Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest("Ocorreram erros de validação.");
            }
        }

        [HttpPost]
        [Route("autenticar")]
        public IActionResult Get(UsuarioAutheticateModel model, [FromServices] IUsuarioRepository rep)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(rep.ObterUsuario(model.Email, model.Senha) != null)
                    {
                        CriptoPassword cripto = new CriptoPassword();
                        var password = cripto.Encrypt(model.Senha);
                        var usuario = rep.ObterUsuario(model.Email, password);
                        var token = TokenServices.GenerateToken(usuario);
                        return Ok();
                        /*
                        return Ok(new
                        {
                            token = token
                        });
                        */
                    }
                    else
                    {
                        return StatusCode(401, "Erro na autenticação");
                    }
                }catch(Exception e){
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest("Ocorreram Erros de validação!");
            }
        }
    }
}
