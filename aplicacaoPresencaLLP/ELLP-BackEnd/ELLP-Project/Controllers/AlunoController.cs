using Microsoft.AspNetCore.Mvc;
using ELLP_Project.Models;
using ELLP_Project.Services;
 
namespace ELLP_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoServices _alunoServices;

        public AlunoController(AlunoServices alunoServices)
        {
            _alunoServices = alunoServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlunoModel>> GetTodos()
        {
            try
            {
                var alunos = _alunoServices.GetAlunos();

                if(!alunos.Any())
                    return NoContent();

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno:"+ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<AlunoModel> GetPorId(int alunoId)
        {
            try
            {
                return Ok(_alunoServices.GetAlunoById(alunoId));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
          
        }

        [HttpPost]
        public ActionResult CreateAluno([FromBody] AlunoModel novoAluno)
        {
            try
            {
                _alunoServices.CadastrarAluno(novoAluno);
                return CreatedAtAction(nameof(GetPorId), new { id = novoAluno.AlunoId }, novoAluno);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro Interno: " + ex.Message);
            }
        }

        [HttpPut("atualizarAluno/{id}")]
        public ActionResult Atualizar(int id, [FromBody] AlunoModel alunoAtualizado)
        {
            try
            {
                _alunoServices.AtualizarAluno(id, alunoAtualizado);
                return Ok("Aluno atualizado.");
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro Interno: " + ex.Message);
            }
        }

        [HttpDelete("excluirAluno/{id}")]
        public ActionResult Deletar(int id)
        {
            try
            {
                _alunoServices.RemoverAluno(id);
                return Ok("Aluno removido.");
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro Interno: " + ex.Message);
            }

        }

        [HttpPut("{id}/nome")]
        public ActionResult AlterarNome(int id, [FromBody] string novoNome)
        {
            try
            {
                var aluno = _alunoServices.GetAlunoById(id);
                aluno.AlunoNome = novoNome;
                _alunoServices.AtualizarAluno(id, aluno);
                return Ok("Nome alterado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro Interno: " + ex.Message);
            }
        }

        [HttpGet("{id}/oficina")]
        public ActionResult <OficinaModel> ListarOficinas(int id)
        {
            try
            {
                var aluno = _alunoServices.GetAlunoById(id);
                return Ok(aluno.AlunoOficinas);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {                                                                               
                return StatusCode(500, "Erro Interno: " + ex.Message);
            }
        }
    }
}
