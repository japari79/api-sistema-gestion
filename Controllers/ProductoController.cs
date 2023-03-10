using api_sistema_gestion.Handle;
using api_sistema_gestion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Producto> getProductosXUsuario(long idUsuario)
        {
            return ProductoHandle.getProductosXUsuario(idUsuario);
        }
        
        [HttpPost]
        public void CrearProducto(Producto producto)
        {
            ProductoHandle.postProducto(producto);
        }

        [HttpPut]
        public void ModificarProducto(Producto producto)
        {
            ProductoHandle.putProducto(producto);
        }

        [HttpDelete("{id}")]
        public void EliminarProducto(long id)
        {
            ProductoHandle.delProducto(id);
        }
    }
}
