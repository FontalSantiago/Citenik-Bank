using System.ComponentModel.DataAnnotations;

namespace Application.Validations
{
    public class OnlyNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var variable = value.GetType();

            if (variable == typeof(string) )
            {
                return new ValidationResult("Solo se admiten números");
            }

            return ValidationResult.Success;
        }
    
    }
}
