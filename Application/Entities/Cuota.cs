
namespace Application.Entities
{
    public class Cuota
    {
        /// <summary>
        /// Id de la Cuota
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Id del Prestamo
        /// </summary>
        public int idPrestamo { get; set; }

        /// <summary>
        /// Numero de Cuota de un Prestamo
        /// </summary>
        public int nroCuota { get; set; }

        /// <summary>
        /// Fecha de Pago de la Cuota
        /// </summary>
        public DateTime? fechaPago { get; set; }

        /// <summary>
        /// Fecha de Vencimiento de la Cuota
        /// </summary>
        public DateTime fechaVencimiento { get; set; }
    }
}
