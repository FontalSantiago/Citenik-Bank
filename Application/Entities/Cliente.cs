using Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities

{
    public class Cliente
    {
        /// <summary>
        /// ID del Cliente
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Legajo del Cliente
        /// </summary>
        //configurar autoincrement
        public int legajo { get; set; }

        /// <summary>
        /// Nombre del Cliente
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "El campo {0} no debe tener mas de {1} y menos de {2} caracteres")]
        [FirstCapitalUpper]
        public string nombre { get; set; }

        /// <summary>
        /// CUIT del Cliente
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 20, MinimumLength = 12, ErrorMessage = "El campo {0} no debe tener mas de {1} y menos de {2} caracteres")]
        public string CUIT { get; set; }

        /// <summary>
        /// Fecha de nacimiento del Cliente.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Date)]
        [ValidationDateBirthday(ErrorMessage = "La fecha ingresada no puede ser superior a la fecha actual.")]
        public DateTime fecha_nacimiento { get; set; }

        /// <summary>
        /// Estado en el que se encuentra la cuenta del Cliente.
        /// </summary>
        public bool estado { get; set; }

    }
}
