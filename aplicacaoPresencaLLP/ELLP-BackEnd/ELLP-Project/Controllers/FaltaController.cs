using Microsoft.AspNetCore.Mvc;
using ELLP_Project.Models;
using ELLP_Project.Services;

namespace ELLP_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaltaController : ControllerBase
    {
        private readonly FaltaServices _faltaServices;

        public FaltaController(FaltaServices faltaServices)
        {
            _faltaServices = faltaServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FaltaModel>> GetAll()
        {
            try
            {
                var faltas = _faltaServices.GetFaltas();
                return Ok(faltas);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro interno: "+ex.Message);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<FaltaModel> GetById(int id)
        {
            try
            {
                return Ok(_faltaServices.GetFaltaById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
            
        }

        [HttpPost]
        public ActionResult CreateFalta([FromBody] FaltaModel falta)
        {
            try
            {
                _faltaServices.CadastrarFalta(falta);
                return CreatedAtAction(nameof(GetById), new { id = falta.FaltaId }, falta);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpPut("atualizarFalta/{id}")]
        public ActionResult Update(int id, [FromBody] FaltaModel falta)
        {
            try
            {
                return Ok(_faltaServices.AtualizarFalta(id, falta));
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

        [HttpDelete("ExcluirFalta/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _faltaServices.RemoverFalta(id);
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
        [HttpGet("FaltasPorAluno/{id}")]
        public ActionResult<List<FaltaModel>> FaltasPorAluno(int alunoId)
        {
            try
            {
                return Ok(_faltaServices.GetFaltasByAluno(alunoId));
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

    }
}
