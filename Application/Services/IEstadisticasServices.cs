using Application.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IEstadisticasServices
    {
        public Task<ActionResult<IList<Object>>> consultaPrestamosActivosPlan();
        public Task<ActionResult<IList<Object>>> consultaPrestamosActivosCapital();
        public Task<ActionResult<IList<Object>>> consultaTotalPorConcepto(int idConcepto);
        public Task<ActionResult<IList<Object>>> consultaTotalPorConceptoInteresPunitorio();
        public Task<ActionResult<IList<Object>>> consultaClientesMayorDeuda();
        public Task<ActionResult<IList<Object>>> consultaPrestamosConMayorRentabilidad();

    }
}
