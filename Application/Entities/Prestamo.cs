using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public enum DiaVencimiento
    {
        diez = 10,
        quince = 15,
        veinte = 20
    }

    public class Prestamo
    {
        /// <summary>
        /// Id del Prestamo
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// Id del Plan
        /// </summary>
        [Required]
        public int idPlan { get; set; }

        /// <summary>
        /// Id del Cliente
        /// </summary>
        [Required]
        public int idCliente { get; set; }

        /// <summary>
        /// Capital del Prestamo
        /// </summary>
        [Required]
        public double capital { get; set; }

        /// <summary>
        /// Cantidad de Cuotas del Prestamo
        /// </summary>
        [Required]
        public int cantidadCuotas { get; set; }

        /// <summary>
        /// Fecha de Otorgamiento del Prestamo
        /// </summary>
        [Required]
        public DateTime fechaOtorgamiento { get; set; }

        /// <summary>
        /// Dia en que venceran las cuotas del Prestamo
        /// </summary>
        [Required]
        public DiaVencimiento diaVencimiento { get; set; } 

        /// <summary>
        /// Estado del Prestamo
        /// </summary>
        public string estado { get; set; }
    }
    
}
