using Application.DTO;
using Application.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class ClientesServices : ControllerBase, IClienteServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientesServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<ClienteDTO>> listarClientes()
        {
            try
            {
                var clientes = await _context.Clientes.ToListAsync();
                return _mapper.Map<IList<ClienteDTO>>(clientes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los clientes", ex);
            }
            
        }
        public async Task<ClienteDTO>obtenerCliente(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync<Cliente>(x => x.id == id);
                return _mapper.Map<ClienteDTO>(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el cliente", ex);
            }
        }
        public async Task<IList<ClienteDTO>> verificarCliente(string CUIT)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.CUIT == CUIT).ToListAsync();
                return _mapper.Map<IList<ClienteDTO>>(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el cliente por ", ex);
            }
        }

        public async Task<ActionResult> guardarCliente(Cliente cliente)
        {
            try
            {
                int legajoMasAlto;
                var existeCliente = await _context.Clientes.AnyAsync(x => x.CUIT == cliente.CUIT);
                if (existeCliente)
                {
                    return BadRequest($"El cliente con el CUIT {cliente.CUIT} ya existe");
                }
                try
                {
                    legajoMasAlto = (from Clientes in _context.Clientes
                                     orderby Clientes.legajo
                                     select Clientes.legajo).Max();
                }
                catch (InvalidOperationException)
                {
                    legajoMasAlto = 999;
                }
                _context.Add(cliente);
                cliente.legajo = legajoMasAlto + 1;
                cliente.estado = true;
                await _context.SaveChangesAsync();
                return Created("/api/clientes", cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar un cliente", ex);
            }
        }

        public async Task<ActionResult> modificarCliente(int id, Cliente cliente)
        {
            try
            {
                if (cliente.id != id)
                {
                    return NotFound("El Id del cliente no esta registrado en el sistema");
                }
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar actualizar los datos de un cliente", ex);
            }
        }
    }
}
