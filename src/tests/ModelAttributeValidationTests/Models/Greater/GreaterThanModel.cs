#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;

#endregion

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanModel
    {
        public int IntNumber { get; set; }

        [ValGreaterThan(5)] 
        public long LongNumber { get; set; }

        [ValGreaterThan(10)] 
        public short ShortNumber { get; set; }

        [ValGreaterThan(1)]
        public ushort UnSignedShortNumber { get; set; }

        [ValGreaterThan(5.69)] 
        public decimal DecimalNumber { get; set; }

        [ValGreaterThan(6.9)] 
        public float FloatNumber { get; set; }

        [ValGreaterThan(56.9)] 
        public double DoubleNumber { get; set; }
    }
}
