using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PrestamoConsulta
    {
        public int id { get; set; }
        public string CUIT { get; set; }
        public string nombre { get; set; }
        public double capital { get; set; }
        public int cantidadCuotas { get; set; }
        public int cuotasPagas { get; set; }
        public int cuotasVencidasImpagas { get; set; }
        public DateTime fechaOtorgamiento { get; set; }
        public string estado { get; set; }
    }
}
