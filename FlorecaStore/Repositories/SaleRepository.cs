using FlorecaStore.Database;
using FlorecaStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FlorecaStore.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            _context.Sale.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            try
            {
                var sale = await _context.Sale.Include(s => s.Items)
                    .OrderByDescending(s => s.Date)
                    .ToListAsync();
                    

                return sale.Cast<Sale>().ToList();
            }
            catch
            {
                throw new Exception("Não foram encontradas vendas registradas");
            }
        }
    }
}
