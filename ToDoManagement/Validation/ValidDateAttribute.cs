using System.ComponentModel.DataAnnotations;
namespace ToDoManagement.Validation
{
    public class ValidDateAttribute : ValidationAttribute
    {
        public ValidDateAttribute() : base("The Due Date cannot be in the past.")
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true; 

            if (value is DateTime dateValue)
            {
                return dateValue >= DateTime.Today;
            }

            return false;
        }
    }
}
