//using System;
//using System.Globalization;
//using System.ComponentModel.DataAnnotations;

//namespace ShipLine.CustomValidator
//{
//    [AttributeUsage(AttributeTargets.Property |
//        AttributeTargets.Field, AllowMultiple = false)]
//    sealed public class CustomAttribute : ValidationAttribute
//    {
//        public override bool IsValid(object value)
//        {
//            bool result = true;

//            return result;
//        }
//        public override string FormatErrorMessage(string name)
//        {
//            string ErrorMessageString = null;
//            return String.Format(CultureInfo.CurrentCulture,
//              ErrorMessageString, name);
//        }
//    }
//}
