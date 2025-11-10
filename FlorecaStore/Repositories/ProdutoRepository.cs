using FlorecaStore.Database;
using FlorecaStore.DTO;
using FlorecaStore.Models;
using FlorecaStore.Models.Auxiliares;
using Microsoft.EntityFrameworkCore;

namespace FlorecaStore.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>> GetAllProdutos()
        {
            var produtos = await _context.Produtos
                .Include(p => p.Entradas)
                .Select(p => new
                {
                    p.Id,
                    p.Quantidade,
                    p.Descricao,
                    p.Nome,
                    p.ValorCusto,
                    p.ValorUnitario,
                    p.ImageUrl,
                    Entradas = p.Entradas.Select(e => new
                    {
                        e.Quantidade,
                        e.Data,
                        e.ValorCusto
                    }).ToList()
                }).ToListAsync();

            return produtos.Cast<object>().ToList();
        }

        public async Task<IEnumerable<Entrada>> GetAllEntradas()
        {
            return await _context.Entradas.ToListAsync();
        }

        public async Task<Produtos> AddProduto(Produtos produto)
        {
            try
            {
                var addProduto = new Produtos()
                {
                    Descricao = produto.Descricao,
                    ImageUrl = produto.ImageUrl,
                    Nome = produto.Nome,
                    Quantidade = produto.Quantidade,
                    ValorUnitario = produto.ValorUnitario,
                    ValorCusto = produto.ValorCusto,
                };

                _context.Produtos.Add(addProduto);
                await _context.SaveChangesAsync();

                return addProduto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Produtos> GetProdutoById(int id)
        {
            try
            {
                var produto = await _context.Produtos
                    .Include(p => p.Entradas)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (produto == null)
                    throw new Exception("Produto não encontrado");

                return produto;
              
            }
            catch
            {
                throw new Exception("Produto não encontrado");
            }
        }

        public Task<IEnumerable<Produtos>> GetProdutoByName(string nome)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Produtos produto)
        {
            try
            {
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

                if (produto == null)
                    throw new Exception("Produto não encontrado");

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddEntrada(Entrada entrada)
        {
            try
            {
                _context.Entradas.Add(entrada);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
