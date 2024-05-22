using Application.Entities;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitenikBank.Controllers
{
    [Produces("application/json")]
    [Route("api/estadisticas")]
    [Authorize]
    [ApiController]
    public class EstadisticasController: ControllerBase
    {
        private readonly IEstadisticasServices _servicioEstadistica;

        public EstadisticasController(IEstadisticasServices servicioEstadistica)
        {
            _servicioEstadistica = servicioEstadistica;
        }

        // GET api/prestamos/id
        /// <summary>
        /// Recupera el prestamo con el ID pasado por parametro.
        /// </summary>
        /// <param name="id">ID del prestamo</param>
        /// <returns>Prestamo</returns>
        [HttpGet("prestamosActivosPlan")]
        [Produces(typeof(Prestamo))]
        public async Task<ActionResult<IList<Object>>> consultaPrestamosActivosPlan()
        {
            var result = _servicioEstadistica.consultaPrestamosActivosPlan();
            return await result;
        }
        // GET api/prestamos/id
        /// <summary>
        /// Recupera el listado de prestamos activos contenidos en un rango de capital dado.
        /// </summary>
        /// <param name="capitalMax"></param>
        /// <param name="capitalMin"></param>
        /// <returns></returns>
        [HttpGet("prestamosActivosCapital")]
        [Produces(typeof(Object))]
        public async Task<ActionResult<IList<Object>>> consultaPrestamosActivosCapital()
        {
            var result = _servicioEstadistica.consultaPrestamosActivosCapital();
            return await result;
        }

        // GET api/prestamos
        /// <summary>
        /// Recupera el monto total del concepto pasado por parámetro.
        /// </summary>
        /// <param name="idConcepto">ID de Concepto</param>
        /// <returns>Monto Total de un Concepto</returns>
        [HttpGet("totalPorConceptoCapital")]
        [Produces(typeof(Object))]
        public async Task<ActionResult<IList<Object>>> consultaTotalPorConceptoCapital()
        {
            var result = _servicioEstadistica.consultaTotalPorConcepto(1);
            return await result;
        }

        // GET api/prestamos
        /// <summary>
        /// Recupera el monto total del concepto pasado por parámetro.
        /// </summary>
        /// <param name="idConcepto">ID de Concepto</param>
        /// <returns>Monto Total de un Concepto</returns>
        [HttpGet("totalPorConceptoInteresFinanciero")]
        [Produces(typeof(Object))]
        public async Task<ActionResult<IList<Object>>> consultaTotalPorConceptoInteres()
        {
            var result = _servicioEstadistica.consultaTotalPorConcepto(2);
            return await result;
        }

        // GET api/prestamos
        /// <summary>
        /// Recupera el monto total del concepto pasado por parámetro.
        /// </summary>
        /// <param name="idConcepto">ID de Concepto</param>
        /// <returns>Monto Total de un Concepto</returns>
        [HttpGet("totalPorConceptoInteresPunitorio")]
        [Produces(typeof(Object))]
        public async Task<ActionResult<IList<Object>>> consultaTotalPorConceptoPunitorio()
        {
            var result = _servicioEstadistica.consultaTotalPorConceptoInteresPunitorio();
            return await result;
        }

        // GET api/prestamos
        /// <summary>
        /// Recupera el Top 5 de Clientes con mayor deuda.
        /// </summary>
        /// <returns>Lista clientes con mayor deuda</returns>
        [HttpGet("clientesMayorDeuda")]
        [Produces(typeof(Object))]
        public async Task<ActionResult<IList<Object>>> consultaClientesMayorDeuda()
        {
            var result = _servicioEstadistica.consultaClientesMayorDeuda();
            return await result;
        }

        // GET api/prestamos
        /// <summary>
        /// Recupera el top 5 de Préstamos con mayor rentabilidad.
        /// </summary>
        /// <returns>Lista prestamos con mayor rentabilidad</returns>
        [HttpGet("prestamosConMayorRentabilidad")]
        [Produces(typeof(Object))]
        public async Task<ActionResult<IList<Object>>> consultaPrestamosConMayorRentabilidad()
        {
            var result = _servicioEstadistica.consultaPrestamosConMayorRentabilidad();
            return await result;
        }
    }
}
