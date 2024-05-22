
namespace Application.Entities
{
    public class Composicion
    {
        /// <summary>
        /// Id de la Composición
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Id de la Cuota
        /// </summary>
        public int idCuota { get; set; }

        /// <summary>
        /// Id del Concepto
        /// </summary>
        public int idConcepto { get; set; }

        /// <summary>
        ///  Monto de la Composición
        /// </summary>
        public double monto { get; set; }
    }
}
