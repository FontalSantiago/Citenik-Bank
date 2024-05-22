using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class Usuario
    {
        /// <summary>
        /// ID del Usuario
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [Required]
        public string usuario { get; set; }

        /// <summary>
        /// Contraseña del Usuario
        /// </summary>
        [Required]
        public string password { get; set; }

    }
}
