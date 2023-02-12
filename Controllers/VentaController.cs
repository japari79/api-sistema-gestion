using api_sistema_gestion.Handle;
using api_sistema_gestion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("{idUsuario}")]
        public void CargarVenta(long idUsuario, List<Producto> productos)
        {
            ProductoVentaHandle.postProductoVenta(idUsuario, productos);
        }
    }
}
