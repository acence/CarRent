using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.UnitTests
{
    public static class ValidationErrorCodes
    {
        public const string GreaterThanOrEqualTo = "GreaterThanOrEqualValidator";
        public const string NotEqualTo = "NotEqualValidator";
        public const string GreaterThan = "GreaterThanValidator";
        public const string LessThan = "LessThanValidator";
        public const string MaximumLength = "MaximumLengthValidator";
        public const string Predicate = "PredicateValidator";
        public const string NotEmpty = "NotEmptyValidator";
        public const string Enum = "EnumValidator";
    }
}
