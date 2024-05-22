namespace Application.DTO
{
    public class ClienteDTO
    {
        public int id { get; set; }
        public int legajo { get; set; }
        public string nombre { get; set; }
        public string CUIT { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public bool estado { get; set; }
    }
}
