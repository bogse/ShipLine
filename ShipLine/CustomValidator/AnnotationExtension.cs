using System.ComponentModel.DataAnnotations;
namespace ShipLine.CustomValidator
{
    public class ValidStartEndDate : ValidationAttribute
    {
        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.VoyageModel)validationContext.ObjectInstance;
            DateTime _endDate = Convert.ToDateTime(value);
            DateTime _startDate = Convert.ToDateTime(model.StartDate);

            if (_startDate > _endDate)
            {
                //need throw?
                return new ValidationResult
                    ("EndDate cannot be lower than StartDate.");
            }
            else if (_startDate < DateTime.Now)
            {
                return new ValidationResult
                    ("Start date cannot be lower than today.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}