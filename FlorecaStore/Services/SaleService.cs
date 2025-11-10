using FlorecaStore.Models;
using FlorecaStore.Repositories;

namespace FlorecaStore.Services
{
    public class SaleService
    {
        private ISaleRepository _saleRepository;

        public SaleService(ISaleRepository salerepository)
        {
            _saleRepository = salerepository;
        }

        public async Task AddAsync(Sale sale)
        {
            await _saleRepository.AddAsync(sale);
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _saleRepository.GetAllAsync();
        }
    }
}
