using FlorecaStore.Models.Auxiliares;
using System.ComponentModel.DataAnnotations;

namespace FlorecaStore.Models
{
    public class Sale
    {
        [Key]
        public string Id { get; set; } = String.Empty;
        public string UserId { get; set; } = String.Empty;
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = String.Empty;
        public DateTimeOffset Date { get; set; }
    }

    
}
