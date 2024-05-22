using System.ComponentModel.DataAnnotations;

namespace Application.Validations
{

    public class ValidationDateBirthday : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            if(value != null)

            { 
            return d <= DateTime.Now; 
            }
            return true;
        }
    }
}
