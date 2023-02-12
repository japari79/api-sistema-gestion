using api_sistema_gestion.Handle;
using api_sistema_gestion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPut]
        public void ModificarUsuario(Usuario usuario)
        {
            UsuarioHandle.putUsuario(usuario);
        }
    }
}
