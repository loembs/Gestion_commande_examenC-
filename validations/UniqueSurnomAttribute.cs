using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Validations
{
    public class UniqueSurnomAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var dbContext = new CommandeDbContext();
            var client = validationContext.ObjectInstance as Client;
            var surnom = value.ToString();

            var clientExistant = dbContext.Clients.FirstOrDefault(c => c.Surnom == surnom);

            if (clientExistant != null && clientExistant.Id != client.Id)
            {
                return new ValidationResult(ErrorMessage ?? "Ce surnom existe déjà");
            }

            return ValidationResult.Success;
        }
    }
}
