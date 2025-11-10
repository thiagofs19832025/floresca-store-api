using FlorecaStore.Database;
using FlorecaStore.DTO;
using FlorecaStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FlorecaStore.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;
        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> LoginUsuarioRepository(LoginDto request)
        {
            try
            {
                var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email);
            
                if (usuario == null)
                    throw new Exception("Usuário não encontrado");

                return usuario;
            }
            catch
            {
                throw new Exception("E-mail informado não corresponde a um usuário cadastrado");
            }
        }
    }
}
