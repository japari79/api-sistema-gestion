using Microsoft.AspNetCore.Http;
using api_sistema_gestion.Handle;
using api_sistema_gestion.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<ProductoVenta> ObtenerProductosVendidosXUsuario(long idUsuario)
        {
            return ProductoVentaHandle.getProductosVendidosXUsuario(idUsuario);
        }
    }
}
