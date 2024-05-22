namespace Application.DTO
{
    public class PlanDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double TNA { get; set; }
        public int CuotasMax { get; set; }
        public int CuotasMin { get; set; }
        public double MontoMax { get; set; }
        public double MontoMin { get; set; }
        public int? EdadMax { get; set; }
        public int? PrecanCuota { get; set; }
        public double? PrecanMulta { get; set; }
        public double? CostoOtorgamiento { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }
    }
}
