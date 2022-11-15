//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;
//using System.Text.RegularExpressions;
//using ShipLine.Models;

//namespace ShipLine.CustomValidator
//{
//    [MetadataType(typeof(VoyageMetadata))]
//    [CustomValidation(typeof(VoyageModel), "FinalCheck")]
//    public partial class Voyage
//    {
//        public class VoyageMetadata
//        {
//            [CustomValidation(typeof(VoyageModel), "TestCategoryName")]
//            public DateTime StartDate { get; set; }
//            public DateTime EndDate { get; set; }
//        }
//        partial void OnValidate()
//        {
//            List<ValidationResult> vErrors = new List<ValidationResult>();

//            // validate class-level attributes
//            // When the last parameter is true, it also evaluates all ValidationAttributes on this entity's properties
//            Validator.TryValidateObject(this, new ValidationContext(this, null, null), vErrors, true);

//            // handle errors
//            if (vErrors.Count > 0)
//                throw new ValidationException(vErrors[0], null, this);

//        }
//    }
//}
