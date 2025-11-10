using FlorecaStore.Models;

namespace FlorecaStore.Repositories
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAllAsync();
        Task AddAsync(Sale sale);
    }
}
