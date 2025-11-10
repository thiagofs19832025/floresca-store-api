using FlorecaStore.Models;
using FlorecaStore.Models.Auxiliares;

namespace FlorecaStore.Repositories
{
    public interface IProdutoRepository
    {
        public Task<List<object>> GetAllProdutos();
        Task<IEnumerable<Entrada>> GetAllEntradas();
        Task AddEntrada(Entrada entrada);
        Task<Produtos> AddProduto(Produtos produto);
        Task<Produtos> GetProdutoById(int id);
        Task<IEnumerable<Produtos>> GetProdutoByName(string nome);
        Task UpdateAsync(Produtos produto);
        Task DeleteAsync(int id);
    }
}
