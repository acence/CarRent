using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Application.Behaviours
{
    public static class ValidationErrorCodes
    {
        public const string GreaterThanOrEqualTo = "GreaterThanOrEqualValidator";
        public const string NotEqualTo = "NotEqualValidator";
        public const string GreaterThan = "GreaterThanValidator";
        public const string LessThan = "LessThanValidator";
        public const string MaximumLength = "MaximumLengthValidator";
        public const string Predicate = "PredicateValidator";
        public const string AsyncPredicate = "AsyncPredicateValidator";
        public const string NotEmpty = "NotEmptyValidator";
        public const string Enum = "EnumValidator";


        public const string NotFound = "NotFoundValidator";
        public const string NotUnique = "NotUniqueValidator";
        public const string TooLong = "TooLongValidator";
        public const string NotAvailable = "NotAvailableValidator";
    }
}
