using System.Text.Json.Serialization;

namespace FlorecaStore.DTO
{
    public class LoginResultDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiraEm { get; set; }
        [JsonPropertyName("user")]
        public UsuarioDto? Usuario { get; set; }
    }
}
