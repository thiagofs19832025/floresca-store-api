using FlorecaStore.DTO;
using FlorecaStore.Models;
using FlorecaStore.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlorecaStore.Services
{
    public class LoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        public async Task<LoginResultDto> Login(LoginDto request)
        {
            var usuario = await _loginRepository.LoginUsuarioRepository(request);

            bool senhaValida = BCrypt.Net.BCrypt.Verify(request.Password, usuario.SenhaHash);
            if (!senhaValida)
                throw new Exception("Senha Inválida no Service");

            var token = GerarToken(usuario);

            var loginResult = new LoginResultDto
            {
                ExpiraEm = DateTime.Now.AddHours(2),
                Token = token,
                Usuario = new UsuarioDto
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    Role = usuario.Role,
                }
            };
            
            return loginResult;
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role.ToString())
            };

            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
