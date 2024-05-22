using Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PlanesConsulta
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double TNA { get; set; }
        public int cuotasMax { get; set; }
        public int cuotasMin { get; set; }
        public double montoMax { get; set; }
        public double montoMin { get; set; }
        public int? edadMax { get; set; }
        public int? precanCuota { get; set; }
        public double? precanMulta { get; set; }
        public double? costoOtorgamiento { get; set; }
        public DateTime vigenciaDesde { get; set; }
        public DateTime? vigenciaHasta { get; set; }
    }
}
