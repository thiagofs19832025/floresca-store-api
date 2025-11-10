using FlorecaStore.DTO;
using FlorecaStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlorecaStore.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            try
            {
                var result = await _loginService.Login(request);

                if (result == null)
                    throw new Exception("Senha Inválida no controller");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            
        }
    }
}
