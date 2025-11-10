using FlorecaStore.DTO;
using FlorecaStore.Models;
using FlorecaStore.Models.Auxiliares;
using FlorecaStore.Repositories;

namespace FlorecaStore.Services
{
    public class ProdutoService
    {
        private IProdutoRepository _produtoRepositoy;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepositoy = produtoRepository;
        }

        public async Task<List<object>>GetAllProdutos()
        {
            var produtos = await _produtoRepositoy.GetAllProdutos();

            return produtos;
        }

        public async Task<Produtos> AddProduto(Produtos produto)
        {
            return await _produtoRepositoy.AddProduto(produto);
        }

        public async Task<Produtos> GetProdutosById(int id)
        {
            return await _produtoRepositoy.GetProdutoById(id);
        }

        public async Task EditProduto(Produtos produto)
        {
            await _produtoRepositoy.UpdateAsync(produto);
        }

        public async Task AddEntrada(Entrada entrada)
        {
            await _produtoRepositoy.AddEntrada(entrada);
        }

        public async Task DeleteProduto(int id)
        {
            await _produtoRepositoy.DeleteAsync(id);
        }
    }
}
