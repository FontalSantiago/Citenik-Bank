using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace CitenikBank.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteServices _servicioClientes;

        public ClientesController(IClienteServices servicioClientes)
        {
            _servicioClientes = servicioClientes;
        }

        // GET: api/clientes
        /// <summary>
        /// Recupera el listado de los clientes registrados en la base de datos de Citenik y por query string, si se pasa un CUIT, busca un cliente por ese CUIT.
        /// </summary>
        /// <returns>Lista de Clientes.</returns>
        [HttpGet]
        [Produces(typeof(List<ClienteDTO>))]
        public async Task<ActionResult<IList<ClienteDTO>>> listarClientes(string CUIT)
        {   
            if ( CUIT is null  )
            {
                var result = await _servicioClientes.listarClientes ();
                return Ok(result);
            }
            else
            {
                var result = await _servicioClientes.verificarCliente(CUIT);
                return Ok(result);
            } 
        }

        // GET api/clientes/id
        /// <summary>
        /// Recupera el cliente con el ID pasado por parametro.
        /// </summary>
        /// <param name="ID">ID del cliente</param>
        /// <returns>Cliente</returns>
        [HttpGet("{id:int}")]
        [Produces(typeof(ClienteDTO))]
        public async Task<ActionResult<ClienteDTO>> obtenerCliente(int id)
        {
            var result = await _servicioClientes.obtenerCliente(id);
            return result;
        }

        // POST api/clientes
        /// <summary>
        /// Registra un nuevo cliente en la base de datos de citenik
        /// </summary>
        /// <param name="clienteCreacionDTO">Cliente que se quiere registrar</param>
        /// <returns>Cliente registrado</returns>
        /// <response code="201">Registra el nuevo cliente</response> /// <response code="400">El cliente ya ha sido registrado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> guardarCliente(Cliente cliente)
        {
            var result = await _servicioClientes.guardarCliente(cliente);
            return result;
        }

        // PUT api/clientes/id
        /// <summary>
        /// Actualiza los datos de un cliente registrado
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <param name="cliente">Cliente cuyos atributos se actualizaran</param>
        /// <returns>Cliente con datos modificados</returns>
        /// <response code="200">Modifica los datos del cliente</response> /// <response code="400">El id del cliente no coincide</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> modificarCliente(int id, Cliente cliente)
        {
            var result = _servicioClientes.modificarCliente(id, cliente);
            return await result;
        }
    }
}
