using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
//using ShipLine.Models;
//using System.ComponentModel.DataAnnotations;

//namespace ShipLine.CustomValidator
//{
//    public class VoyageModelValidator : AbstractValidator<VoyageModel>
//    {
//        public VoyageModelValidator()
//        {
//            RuleFor(m => m.StartDate)
//                .GreaterThanOrEqualTo(DateTime.Now)
//                .WithMessage("Start Date is Required");
//            RuleFor(m => m.EndDate)
//                .GreaterThan(m => m.StartDate)
//                .WithMessage("End date must after Start date");
//        }
//    }
//}
