using Application;
using Application.DTO;
using Application.Entities;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitenikBank.Controllers
{

    [Produces("application/json")]
    [Route("api/prestamos")]
    [Authorize]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamosServices _servicioPrestamo;
        public PrestamosController(IPrestamosServices servicioPrestamo)
        {
            _servicioPrestamo = servicioPrestamo;
        }

        // GET: api/prestamos/consulta
        /// <summary>
        /// Recupera el listado de los prestamos registrados en la base de datos de Citenik.
        /// </summary>
        /// <returns>Lista de prestamos</returns>
        [HttpGet]
        [Produces(typeof(List<Prestamo>))]
        public async Task<ActionResult<IList<PrestamoConsulta>>> listarPrestamos()
        {
            var result = _servicioPrestamo.listarPrestamos();
            return await result;
        }

        // GET api/prestamos/id
        /// <summary>
        /// Recupera el prestamo con el ID pasado por parametro.
        /// </summary>
        /// <param name="id">ID del prestamo</param>
        /// <returns>Prestamo</returns>
        [HttpGet("{id:int}")]
        [Produces(typeof(Prestamo))]
        public async Task<ActionResult<Prestamo>> obtenerPrestamo(int id)
        {
            var result = _servicioPrestamo.obtenerPrestamo(id);
            return await result;
        }

        // POST 
        /// <summary>
        /// Registra un nuevo prestamo en la base de datos de Citenik
        /// </summary>
        /// <param name="prestamo">Prestamo que se quiere registrar</param>
        /// <returns>Prestamo registrado</returns>
        /// <response code="201">Registra el nuevo prestamo</response> ///
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> guardarPrestamo(Prestamo prestamo)
        {
            var result = _servicioPrestamo.guardarPrestamo(prestamo);
            return await result;
        }

        // PATCH 
        /// <summary>
        /// Actualiza el estado de un prestamo a vigente en la base de datos Citenik
        /// </summary>
        /// <param name="id">Id del prestamo que se desea actualizar</param>
        /// <param name="prestamo">Prestamo que se quiere actualizar</param>
        /// <returns>Prestamo actualizado</returns>
        /// <response code="200">Registra la actualizaciÃ³n del prestamo</response> /// 
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> actualizarPrestamo(int id)
        {
            var result = _servicioPrestamo.actualizarPrestamo(id);
            return await result;
        }
    }
}