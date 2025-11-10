using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlorecaStore.Models.Auxiliares
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SaleId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string Nome { get; set; } = String.Empty;
        public int Quantidade { get; set; }
        public decimal UnitPrice { get; set; }
        [JsonIgnore]
        [ForeignKey("SaleId")]
        public Sale? Sale { get; set; }
    }
}
