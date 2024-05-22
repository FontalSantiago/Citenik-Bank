using Application.Entities;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.DTO;

namespace CitenikBank.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/prestamo/{idPrestamo:int}/cuotas")]
    [ApiController]
    public class CuotasController : ControllerBase
    {
        private readonly ICuotaServices _servicioCuotas;

        public CuotasController(ApplicationDbContext context, ICuotaServices servicioCuotas)
        {
            _servicioCuotas = servicioCuotas;
        }

        /// <summary>
        /// Recupera el listado de las cuotas de un prestamo particular sumando todas sus composiciones.
        /// </summary>
        /// <param name="idPrestamo">Id del prestamo</param>
        /// <returns>Lista de cuotas de un prestamo particular con sus composiciones</returns>
        [HttpGet]
        [Produces(typeof(List<CuotasComposiciones>))]
        public async Task<ActionResult<IList<CuotasComposiciones>>> listarCuotas(int idPrestamo)
        {
            var result = await _servicioCuotas.listarCuotas(idPrestamo);
            return result;
        }

        /// <summary>
        /// Recupera el listado de las composiciones de una cuota particular.
        /// </summary>
        /// <param name="idCuota">Id de la cuota</param>
        /// <returns>Lista de composiciones de una cuota</returns>
        [HttpGet("{idCuota:int}")]
        [Produces(typeof(List<ComposicionDetallada>))]
        public async Task<ActionResult<IList<ComposicionDetallada>>> listarComposicionesCuota(int idPrestamo, int idCuota)
        {
            var result = await _servicioCuotas.listarComposicionesCuota(idPrestamo, idCuota);
            return result;
        }

    }


}