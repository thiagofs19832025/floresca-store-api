using FlorecaStore.Models;
using FlorecaStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlorecaStore.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private SaleService _saleService;

        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddVenda([FromBody] Sale sale)
        {
            if (sale == null || sale.Items == null || !sale.Items.Any())
                return BadRequest("Venda ou itens inválidos");

            await _saleService.AddAsync(sale);

            return Ok(sale);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllSalesAsync()
        {
            var sale = await _saleService.GetSalesAsync();
            return Ok(sale);
        }
    }
}
