using FlorecaStore.DTO;
using FlorecaStore.Models;

namespace FlorecaStore.Repositories
{
    public interface IUsuarioRepository
    {
        
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        Task<Usuario> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetByEmailAsync(string email);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
    }
}
