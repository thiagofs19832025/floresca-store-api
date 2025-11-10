using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlorecaStore.Models.Auxiliares
{
    public class Entrada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
        public DateTimeOffset Data { get; set; }
        [JsonIgnore]
        public Produtos? Produto { get; set; }
    }
}
