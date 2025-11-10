using FlorecaStore.Models.Auxiliares;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlorecaStore.Models
{
    public class Produtos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Nome { get; set; } = String.Empty;
        public string Descricao { get; set; } = String.Empty;
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal ValorCusto { get; set; }
        [Required]
        public decimal ValorUnitario { get; set; }
        public string ImageUrl { get; set; } = String.Empty;

        public List<Entrada> Entradas { get; set; } = new List<Entrada>();
    }
}
