using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public interface IClienteServices
    {
        public Task<IList<ClienteDTO>> listarClientes();

        public Task<ClienteDTO>obtenerCliente(int id);
        public Task<IList<ClienteDTO>> verificarCliente(string CUIT);

        public Task<ActionResult> guardarCliente(Cliente cliente);

        public Task<ActionResult> modificarCliente(int id, Cliente cliente);
    }
}
