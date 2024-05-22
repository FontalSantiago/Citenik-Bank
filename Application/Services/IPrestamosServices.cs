using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public interface IPrestamosServices
    {
        public Task<ActionResult<IList<PrestamoConsulta>>> listarPrestamos();
        public Task<ActionResult<Prestamo>> obtenerPrestamo(int id);
        public Task<ActionResult> guardarPrestamo(Prestamo prestamo);
        public Task<ActionResult> actualizarPrestamo(int id);
    }
}