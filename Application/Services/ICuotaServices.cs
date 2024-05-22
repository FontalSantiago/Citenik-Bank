using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICuotaServices
    {
        public Task<ActionResult<IList<CuotasComposiciones>>> listarCuotas(int idPrestamo);
        public Task<ActionResult<IList<ComposicionDetallada>>> listarComposicionesCuota(int idPrestamo, int idCuota);
        public Task<ActionResult> guardarComposicion(Cuota cuota, Plan plan, Prestamo prestamo);
        public Task crearComposicion(int idCuota, int idConcepto, double monto);
    }
}