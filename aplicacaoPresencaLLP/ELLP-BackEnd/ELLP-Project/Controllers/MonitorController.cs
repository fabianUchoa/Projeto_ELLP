using Microsoft.AspNetCore.Mvc;
using ELLP_Project.Models;
using ELLP_Project.Services;

namespace ELLP_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitorController : ControllerBase
    {
        private readonly MonitorServices _monitorServices;

        public MonitorController(MonitorServices monitorServices)
        {
            _monitorServices = monitorServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MonitorModel>> GetAll()
        {
            try
            {
                return Ok(_monitorServices.GetMonitors());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MonitorModel> GetById(int id)
        {
            try
            {
                return Ok(_monitorServices.GetMonitorById(id));
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
        public ActionResult Add([FromBody] MonitorModel monitor)
        {
            try
            {
                _monitorServices.CadastrarMonitor(monitor);
                return CreatedAtAction(nameof(GetById), new { id = monitor.Id }, monitor);
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

        [HttpPut("AtualizarMonitor/{id}")]
        public ActionResult Update(int id, [FromBody] MonitorModel monitorAtualizado)
        {
            try
            {
                _monitorServices.AtualizarMonitor(id, monitorAtualizado);
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

        [HttpDelete("ExcluirMonitor/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _monitorServices.RemoverMonitor(id);
                return Ok("Monitor excluído");
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

        [HttpPut("AtualizarLogin/{id}")]
        public ActionResult AtualizarLogin(int monitorId, [FromBody]string login)
        {
            try
            {
                _monitorServices.AtualizarLogin(monitorId, login);
                return Ok("Login atualizado!");
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

        [HttpPut("AtualizarSenha/{id}")]
        public ActionResult AtualizarSenha(int monitorId, [FromBody]string senha)
        {
            try
            {
                _monitorServices.AtualizarSenha(monitorId, senha);
                return Ok("Senha atualizada!");
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

        [HttpPut("AlterarOficina/{id}")]
        public ActionResult AlterarOficinaVinculada(int monitorId, int oficinaId)
        {
            try
            {
                _monitorServices.AlterarOficinaVinculada(monitorId, oficinaId);
                return Ok("Oficina alterada.");
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }
    }
}

