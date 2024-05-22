using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public interface IPagosServices
    {
        public Task<ActionResult<CuotasVencidas>> obtenerCuotaPagar(int idPrestamo, DateTime fechaPago);
        public Task<ActionResult<Double>> pagarCuota(int idPrestamo, DateTime fechaPago);
        
    }    
}
