using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace CitenikBank.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/planes")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private readonly IPlanesServices _servicioPlanes;

        // GET: api/<PlanesController>

        public PlanesController(IPlanesServices servicioPlanes)
        {
            _servicioPlanes = servicioPlanes;
        }

        // GET: api/planes
        /// <summary>
        /// Recupera el listado de los planes registrados en la base de datos de Citenik y por query string, si se pasa un nombre, busca ese plan por nombre.
        /// </summary>
        /// <returns>Lista de Planes.</returns>        
        [HttpGet]
        [Produces(typeof(List<PlanDTO>))]
        public async Task<ActionResult<List<PlanesConsulta>>> listarPlanes(string nombre)
        {
            if (nombre is null)
            {
                var result = await _servicioPlanes.listarPlanes();
                return result;
            }
            else
            {
                var result = await _servicioPlanes.obtenerPlanNombre(nombre);
                return result;
            }
            
        }

        // GET api/planes/id
        /// <summary>
        /// Recupera el plan con el ID pasado por parÃ¡metro.
        /// </summary>
        /// <param name="id">ID del plan</param>
        /// <returns>Plan</returns>
        [HttpGet("{id:int}")]
        [Produces(typeof(PlanDTO))]
        public async Task<ActionResult<PlanDTO>> obtenerPlan(int id)
        {
            var result = await _servicioPlanes.obtenerPlan(id);
            return result;
        }

        // POST api/planes
        /// <summary>
        /// Registra un nuevo plan en la base de datos de citenik
        /// </summary>
        /// <param name="plan">Plan que se quiere registrar</param>
        /// <returns>Plan registrado</returns>
        /// <response code="201">Registra el nuevo plan</response> /// <response code="400">El plan ya ha sido registrado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> guardarPlan(Plan plan)
        {
            var result = await _servicioPlanes.guardarPlan(plan);
            return result;
        }

        // Post api/planes/id
        /// <summary>
        /// Actualiza los datos de un plan registrado
        /// </summary>
        /// <param name="id">ID del plan</param>
        /// <param name="plan">Plan cuyos atributos se actualizaran</param>
        /// <returns>Plan con datos modificados</returns>
        /// <response code="200">Modifica los datos del plan</response> /// <response code="400">El id del plan no coincide</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> modificarPlan(int id, Plan plan)
        {
            var result = await _servicioPlanes.modificarPlan(id, plan);
            return result;
        }
    }
}