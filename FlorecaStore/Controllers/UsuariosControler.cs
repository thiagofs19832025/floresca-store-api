using FlorecaStore.DTO;
using FlorecaStore.Models;
using FlorecaStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlorecaStore.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosControler : ControllerBase
    {
        private readonly UsuarioSevice _usuarioSevice;
        
        public UsuariosControler(UsuarioSevice usuarioService)
        {
            _usuarioSevice = usuarioService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BuscarUsuarios()
        {
            return Ok(await _usuarioSevice.ListarUsuarios());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdicionarUsuario(Usuario usuario)
        {
            try
            {
                await _usuarioSevice.AdicionarUsuarioAsync(usuario);
                return Ok($"Usuário {usuario.Nome} criado com sucesso");
            }
            catch (Exception ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BuscarUsuarioPorId(int id)
        {
            try
            {
                var usuario = await _usuarioSevice.BuscarUsuario(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpGet("buscar-por-email{email}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BuscarUsuarioPorEmail(string email)
        {
            try
            {
                var usuario = await _usuarioSevice.BuscarUsuarioPorEmail(email);

                if (usuario.Count() == 1)
                {
                    return Ok(usuario.First());
                }
                
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AtualizarUsuario(int id, UsuarioDto usuario)
        {
            try
            {
                await _usuarioSevice.AtualizarUsuario(id, usuario);
                return Ok("Usuário atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ApagarUsuario(int id)
        {
            try
            {
                await _usuarioSevice.ApagarUsuario(id);
                return Ok("Usuário deletado com sucesso");
            }
            catch (Exception ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }
    }
}
