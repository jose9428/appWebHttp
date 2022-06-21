using appWebAPI.DAO;
using appWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        [HttpGet("paises")]
        public async Task<ActionResult> GetPaises()
        {
            return Ok(await Task.Run(() => new PaisDAO().listado()));
        }

        [HttpGet("sellers")]
        public async Task<ActionResult> GetSellers()
        {
            return Ok(await Task.Run(() => new SellerDAO().listado()));
        }

        [HttpGet("buscar")]
        public async Task<ActionResult> GetBuscar(string codigo)
        {
            return Ok(await Task.Run(() => new SellerDAO().buscar(codigo)));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> PostAgregar(Seller reg)
        {
            var mensaje = await Task.Run(() => new SellerDAO().agregar(reg));
            return Ok(mensaje);
        }

        [HttpPut("actualizar")]
        public async Task<ActionResult> PutActualizar(Seller reg)
        {
            var mensaje = await Task.Run(() => new SellerDAO().Actualizar(reg));
            return Ok(mensaje);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult> Delete(string codigo)
        {
            var mensaje = await Task.Run(() => new SellerDAO().Elimina(codigo));
            return Ok(mensaje);
        }
    }
}
