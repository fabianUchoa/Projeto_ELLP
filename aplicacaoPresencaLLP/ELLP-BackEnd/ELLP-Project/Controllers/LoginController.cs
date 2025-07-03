using ELLP_Project.Models;
using ELLP_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELLP_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginServices _loginServices;

        public LoginController(LoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpPost]
        public IActionResult AutenticacaoUsuario([FromBody] LoginModel login) { 
            try
            {
                _loginServices.ValidaçãoLogin(login.login, login.senha);
                return NoContent();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }   
        }
    }
}