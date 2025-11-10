using FlorecaStore.DTO;
using FlorecaStore.Models;

namespace FlorecaStore.Repositories
{
    public interface ILoginRepository
    {
        Task<Usuario> LoginUsuarioRepository(LoginDto request);
    }
}
