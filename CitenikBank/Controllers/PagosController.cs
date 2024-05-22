using Application.DTO;
using Application.Entities;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitenikBank.Controllers
{
    [Produces("application/json")]
    [Route("api/pagos")]
    [Authorize]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagosServices _servicioPago;
        public PagosController(IPagosServices servicioPago)
        {
            _servicioPago = servicioPago;
        }

        // GET: api/pagos
        /// <summary>
        /// Recupera la cuota más antigua a pagar de un préstamo registrado en la base de datos de Citenik.
        /// </summary>
        /// <param name="idPrestamo">Id del prestamo al que corresponde la cuota a pagar</param>
        /// <param name="fechaPago">Fecha de Pago de la cuota adeudada</param>
        /// <returns>Monto de la cuota a pagar</returns>
        [HttpGet]
        [Produces(typeof(List<Prestamo>))]
        public async Task<ActionResult<CuotasVencidas>> obtenerCuotaPagar(int idPrestamo, DateTime fechaPago)
        {
            var result = _servicioPago.obtenerCuotaPagar(idPrestamo, fechaPago);
            return await result;
        }

        // POST api/pagos
        /// <summary>
        /// Registra el pago de la cuota en la base de datos de citenik
        /// </summary>
        /// <param name="idPrestamo">Id del prestamo al que corresponde la cuota a pagar</param>
        /// <param name="fechaPago">Fecha de Pago de la cuota adeudada</param>
        /// <returns>Cuota pagada</returns>
        /// <response code="201">Registra el pago de una cuota adeudada</response> /// <response code="400">La cuota ha sido pagada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Double>> pagarCuota(int idPrestamo, DateTime fechaPago)
        {
            var result = _servicioPago.pagarCuota(idPrestamo, fechaPago);
            return await result;
        }
    }
}
