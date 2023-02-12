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
        [HttpGet("{usuario}/{contrasena}")]
        public Usuario IniciarSesion(string usuario, string contrasena)
        {
            return UsuarioHandle.Login(usuario, contrasena);
        }

        [HttpGet("{usuario}")]
        public Usuario ConsultarUsuario(string usuario)
        {
            return UsuarioHandle.getUsuario(usuario);
        }

        [HttpPost]
        public void CrearUsuario(Usuario usuario)
        {
            UsuarioHandle.postUsuario(usuario);
        }

        [HttpPut]
        public void ModificarUsuario(Usuario usuario)
        {
            UsuarioHandle.putUsuario(usuario);
        }
    }
}
