#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.Positive
{
    public class PositiveSbyteModel
    {
        [ValRequiredPositive] 
        public sbyte SbyteNumber { get; set; }
    }

    public class PositiveShortNegativeModel
    {
        [ValRequiredPositive]
        public short ShortNumber { get; set; }
    }

    public class PositiveIntNegativeModel
    {
        [ValRequiredPositive] 
        public int IntNumber { get; set; }
    }

    public class PositiveLongNegativeModel
    {
        [ValRequiredPositive] 
        public long LongNumber { get; set; }
    }

    public class PositiveDecimalNegativeModel
    {
        [ValRequiredPositive]
        public decimal DecimalNumber { get; set; }
    }

    public class PositiveDoubleNegativeModel
    {
        [ValRequiredPositive] 
        public double DoubleNumber { get; set; }
    }
}
