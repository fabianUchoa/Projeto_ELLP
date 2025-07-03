using Microsoft.AspNetCore.Mvc;
using ELLP_Project.Models;
using ELLP_Project.Services;

namespace ELLP_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorServices _professorServices;

        public ProfessorController(ProfessorServices professorServices)
        {
            _professorServices = professorServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProfessorModel>> GetAll()
        {
            try
            {
                return Ok(_professorServices.GetProfessores());
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProfessorModel> GetById(int id)
        {
            try
            {
                return Ok(_professorServices.GetProfessorById(id));
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

        [HttpPost]
        public ActionResult AdicionarProfessor([FromBody] ProfessorModel professor)
        {
            try
            {
                _professorServices.CadastrarProfessor(professor);
                return Ok("Professor cadastrado.");
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

        [HttpPut("AtualizarProfessor/{id}")]
        public ActionResult AtualizarProfessor(int id, [FromBody] ProfessorModel professorAtualizado)
        {
            try
            {
                return Ok(_professorServices.AtualizarProfessor(id, professorAtualizado));
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

        [HttpDelete("ExcluirProfessor/{id}")]
        public ActionResult DeletarProfessor(int id)
        {
            try
            {
                _professorServices.RemoverProfessor(id);
                return Ok("Professor excluído.");
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

        [HttpPut("AlterarSenha/{id}")]
        public IActionResult AlterarSenha(int id, [FromBody] string senha)
        {
            try
            {
                _professorServices.AtualizarSenha(id, senha);
                return Ok("Senha atualizada.");
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

        [HttpPut("AlterarLogin{id}")]
        public IActionResult AlterarLogin(int professorId, [FromBody] string login)
        {
            try
            {
                _professorServices.AtualizarLogin(professorId, login);
                return Ok("Login atualizado.");
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
