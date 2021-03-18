using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private IRepository _repo;
        public ClientesController(IRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetClientes();
                return Ok(result);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{ClienteId}")]
        public async Task<IActionResult> GetById(long ClienteId)
        {
            try
            {
                var result = await _repo.GetClienteById(ClienteId);
                return Ok(result);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.Id}", model);
                }                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> Put(int ClienteId, Cliente model)
        {
            try
            {
                 var cliente = await _repo.GetClienteById(ClienteId);

                if(cliente == null) return NotFound();
                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    cliente = await _repo.GetClienteById(ClienteId);
                    return Created($"/api/cliente/{model.Id}", model);
                } 
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpDelete("{ClienteId}")]
        public async Task<IActionResult> Delete(int ClienteId)
        {
            try
            {
                var cliente = await _repo.GetClienteById(ClienteId);

                if(cliente == null) return NotFound();
                _repo.Delete(cliente);

                if(await _repo.SaveChangesAsync())
                {
                    
                    return Ok();
                } 
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }
    }
}