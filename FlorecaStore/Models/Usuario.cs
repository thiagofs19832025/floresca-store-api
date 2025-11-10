using FlorecaStore.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlorecaStore.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Nome { get; set; } = String.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string? SenhaHash { get; set; } = String.Empty;
        [Required]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }
}
