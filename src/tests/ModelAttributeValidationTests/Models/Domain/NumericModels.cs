#region U S I N G

using RzR.Validation.Attributes.Attributes.Numeric;

#endregion

namespace ModelAttributeValidationTests.Models.Domain
{

    public class DecimalPrecisionModel
    {
        [ValDecimalPrecision(5, 2)] 
        public decimal? Amount { get; set; }
    }

    public class MultipleOfIntModel
    {
        [ValMultipleOf(5)] 
        public int Quantity { get; set; }
    }

    public class MultipleOfDecimalModel
    {
        [ValMultipleOf(0.5)] 
        public decimal Price { get; set; }
    }

    public class MultipleOfNullableModel
    {
        [ValMultipleOf(5)] 
        public decimal? Value { get; set; }
    }
}
