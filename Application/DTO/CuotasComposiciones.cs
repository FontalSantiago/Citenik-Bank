using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CuotasComposiciones
    {
        public int id { get; set; }
        public int nroCuota { get; set; }
        public double monto { get; set; }
        public DateTime? fechaPago { get; set; }
        public DateTime fechaVen { get; set; }
    }
}
