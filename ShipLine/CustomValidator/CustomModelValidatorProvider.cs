//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
//using ShipLine.Models;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;

//namespace ShipLine.CustomValidator
//{
//    public class CustomModelValidatorProvider : IModelValidatorProvider
//    {
//        public void CreateValidators(ModelValidatorProviderContext context)
//        {
//            if (context.Results.Any(v => v.Validator.GetType() == typeof(MyCustomModelValidator) == true))
//            {
//                return;
//            }
//            if (context.ModelMetadata.ContainerType == typeof(VoyageModel))
//            {
//                context.Results.Add(new ValidatorItem
//                {
//                    Validator = new MyCustomModelValidator(),
//                    IsReusable = true
//                });
//            }
//        }
//    }

//    public class MyCustomModelValidator : IModelValidator
//    {
//        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
//        {
//            var model = context.Container as VoyageModel;

//            if(context.ModelMetadata.ModelType == typeof(DateTime)
//                && context.ModelMetadata.Name == nameof(model.StartDate))
//            {
//                if (model.StartDate < DateTime.Now)
//                {
//                    return new List<ModelValidationResult>
//                    {
//                        new ModelValidationResult("", "Start date should be greater than today")
//                    };
//                }
//            }

//            if(context.ModelMetadata.ModelType == typeof(DateTime)
//                && context.ModelMetadata.Name == nameof(model.StartDate))
//            {
//                if (model.StartDate >= model.EndDate)
//                {
//                    return new List<ModelValidationResult>
//                    {
//                        new ModelValidationResult("","Start date should be lower than End date")
//                    };
//                }
//            }

//            return Enumerable.Empty<ModelValidationResult>();
//        }
//    }
//}
