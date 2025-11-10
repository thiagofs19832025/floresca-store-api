using FlorecaStore.DTO;
using FlorecaStore.Models;
using FlorecaStore.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FlorecaStore.Services
{
    public class UsuarioSevice
    {
        private readonly IUsuarioRepository _repository;
        
        public UsuarioSevice(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioDto>> ListarUsuarios()
        {
            var usuarios = await _repository.GetAllAsync();
            var listaUsuarios = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            return listaUsuarios;
        }

        public Task AdicionarUsuarioAsync(Usuario usuario)
        {
            return _repository.AddAsync(usuario);
        }

        public async Task<UsuarioDto> BuscarUsuario(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            return new UsuarioDto()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Role = usuario.Role
            };
        }

        public async Task<IEnumerable<UsuarioDto>> BuscarUsuarioPorEmail(string email)
        {
            var usuario = await _repository.GetByEmailAsync(email);
            var listaUsuarios = usuario.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            return listaUsuarios;
        }

        public async Task AtualizarUsuario(int id, UsuarioDto usuarioUpdate)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            if (!string.IsNullOrEmpty(usuarioUpdate.Nome))
                usuario.Nome = usuarioUpdate.Nome;

            if (!string.IsNullOrEmpty(usuarioUpdate.Email))
                usuario.Email = usuarioUpdate.Email;

            usuario.Role = usuarioUpdate.Role;

            if (!string.IsNullOrEmpty(usuarioUpdate.SenhaHash))
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioUpdate.SenhaHash);

            await _repository.UpdateAsync(usuario);
        }

        public async Task ApagarUsuario(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
