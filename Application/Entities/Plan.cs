using Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class Plan : IValidatableObject
    {
        /// <summary>
        /// ID del Plan
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Nombre del Plan
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string nombre { get; set; }
        /// <summary>
        /// TNA (tasa nominal anual).
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [RegularExpression("^[0-9]+([,])?([0-9]+)?$", ErrorMessage = "* Solo se permiten números.")]
        [Range(0, 999.99, ErrorMessage = "El Campo {0} debe ser un numero entre {1} y {2}")]
        [OnlyNumber]
        public double TNA { get; set; }
        /// <summary>
        /// Cantidad máxima de cuotas.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int cuotasMax { get; set; }

        /// <summary>
        /// Cantidad mínima de cuotas.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int cuotasMin { get; set; }

        /// <summary>
        /// Monto máximo del plan.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        
        public double montoMax { get; set; }

        /// <summary>
        /// Monto mínimo del plan.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        
        public double montoMin { get; set; }

        /// <summary>
        /// Edad máxima admisible para el otorgamiento de un plan.
        /// </summary>
       
        [Range(0, 120)]
        public int? edadMax { get; set; }

        /// <summary>
        /// Cantidad de cuotas permitidas para precancelar.
        /// </summary>
        [Range(1, 623)]
        public int? precanCuota { get; set; }

        /// <summary>
        /// Multa otorgada por precancelación.
        /// </summary>
        
        public double? precanMulta { get; set; }

        /// <summary>
        /// Costo de otorgamiento del plan
        /// </summary>
        
        public double? costoOtorgamiento { get; set; }

        /// <summary>
        /// Fecha inicio de vigencia.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Date)]
        public DateTime vigenciaDesde { get; set; }

        /// <summary>
        /// Fecha fin de vigencia.
        /// </summary>
        [DataType(DataType.Date)]
        [ValidationDateUntil(ErrorMessage = "La fecha ingresada no puede ser inferior a la fecha actual.")]
        public DateTime? vigenciaHasta { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (cuotasMin > cuotasMax)
            {
                yield return new ValidationResult("La cantidad de cuotas minimas debe ser igual o inferior a las cuotas maximas", new string[] { nameof(cuotasMin) });
            }

            if (montoMin > montoMax)
            {
                yield return new ValidationResult("El monto minimo debe ser inferior al monto maximo", new string[] { nameof(montoMin) });
            }
        }
    }
}