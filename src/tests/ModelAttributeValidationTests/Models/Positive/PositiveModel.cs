#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.Positive
{
    public class PositiveModel
    {
        public int IntNumber { get; set; }

        [ValRequiredPositive] 
        public long LongNumber { get; set; }

        [ValRequiredPositive] 
        public short ShortNumber { get; set; }

        [ValRequiredPositive] 
        public ushort UnSignedShortNumber { get; set; }

        [ValRequiredPositive]
        public decimal DecimalNumber { get; set; }

        [ValRequiredPositive]
        public float FloatNumber { get; set; }

        [ValRequiredPositive]
        public double DoubleNumber { get; set; }
    }
}
