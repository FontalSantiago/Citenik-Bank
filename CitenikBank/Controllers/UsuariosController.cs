using Application.Services;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CitenikBank.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServices _servicioUsuarios;

        public UsuariosController(IUsuariosServices servicioUsuarios)
        {
            _servicioUsuarios = servicioUsuarios;
        }

        // POST: api/usuario/login
        /// <summary>
        /// Verifica que el usuario que quiere iniciar sesión sea un usuario registrado y otorga un Token de autenticación.
        /// </summary>
        /// <param name="usuario">Usuario que desea Iniciar Sesión.</param>
        /// <returns>Token Jwt</returns>
        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion(Usuario usuario)
        {
            var result = _servicioUsuarios.IniciarSesion(usuario);
            return result;
        }
    }
}