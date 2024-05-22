using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CuotasVencidas
    {
        public int id { get; set; }
        public int nroCuota { get; set; }
        public double monto { get; set; }
        public DateTime fechaVen { get; set; }
        public DateTime? fechaPago { get; set; }
        public double? montoPagar { get; set; }
        public double? interesPunitorio { get; set; }
    }
}
