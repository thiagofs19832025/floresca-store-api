using FlorecaStore.Models.Auxiliares;
using System.ComponentModel.DataAnnotations;

namespace FlorecaStore.DTO
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = String.Empty;
        public string Descricao { get; set; } = String.Empty;
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal ValorUnitario { get; set; }
        public string ImageUrl { get; set; } = String.Empty;
        public ICollection<List<Entrada>>? Entradas { get; set; }
    }
}
