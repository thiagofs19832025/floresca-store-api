using FlorecaStore.Models;
using FlorecaStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlorecaStore.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProdutos()
        {
            return Ok(await _produtoService.GetAllProdutos());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProduto([FromBody] Produtos produto)
        {
            try
            {
                var novoProduto = await _produtoService.AddProduto(produto);
                return Ok(novoProduto);

            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetProdutoById(int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutosById(id);
                return Ok(produto);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> AtualizaProduto(Produtos produto)
        {
            try
            {
                await _produtoService.EditProduto(produto);
                return Ok($"Produto {produto.Nome} atualizado com sucesso");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                await _produtoService.DeleteProduto(id);
                return Ok($"Produto apagado com sucesso");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
