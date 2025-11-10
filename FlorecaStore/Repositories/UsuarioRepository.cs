using FlorecaStore.Database;
using FlorecaStore.DTO;
using FlorecaStore.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FlorecaStore.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            try
            {
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);
                var addusuario = new Usuario()
                {
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    Role = usuario.Role,
                    SenhaHash = usuario.SenhaHash
                };
                _context.Usuarios.Add(addusuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                if (pgEx.ConstraintName == "IX_Usuarios_Nome")
                    throw new Exception($"Já existe um usuário com o nome {usuario.Nome}.");
                if (pgEx.ConstraintName == "IX_Usuarios_Email")
                    throw new Exception($"Já existe um usuário com o e-mail {usuario.Email}.");

                throw new Exception("Violação de chave única no banco de dados.");
            }


        }
        
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado");
            
                return usuario;
            }
            catch
            {
                throw new Exception("Id informado não corresponde a um usuário cadastrado");
            }
        }

        public async Task<IEnumerable<Usuario>> GetByEmailAsync(string email)
        {
            try
            {
                var usuario = await _context.Usuarios.Where(u => EF.Functions.ILike(u.Email, $"%{email}%")).ToListAsync();

                if (usuario == null || usuario.Count == 0)
                    throw new Exception("Usuário não encontrado");

                return usuario;
            }
            catch
            {
                throw new Exception($"Nome {email} informado não corresponde a um usuário cadastrado");
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    if (pgEx.ConstraintName == "IX_Usuarios_Nome")
                    throw new Exception($"Já existe um usuário com o nome {usuario.Nome}.");
                    if (pgEx.ConstraintName == "IX_Usuarios_Email")
                        throw new Exception($"Já existe um usuário com o e-mail {usuario.Email}.");

                    throw new Exception("Violação de chave única no banco de dados.");
                }
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado");

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
