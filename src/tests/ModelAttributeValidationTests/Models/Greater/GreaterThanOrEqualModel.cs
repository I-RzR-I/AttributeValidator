#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanOrEqualIntModel
    {
        [ValGreaterThanOrEqual(5)] 
        public int Value { get; set; }
    }

    public class GreaterThanOrEqualDecimalModel
    {
        [ValGreaterThanOrEqual(5.5)] 
        public decimal Value { get; set; }
    }

    public class GreaterThanOrEqualDoubleModel
    {
        [ValGreaterThanOrEqual(3.14)] 
        public double Value { get; set; }
    }

    public class GreaterThanOrEqualDateTimeModel
    {
        [ValGreaterThanOrEqual("2024-01-01")] 
        public DateTime Value { get; set; }
    }

    public class GreaterThanOrEqualNullableIntModel
    {
        [ValGreaterThanOrEqual(0)] 
        public int? Value { get; set; }
    }

    public class GreaterThanOrEqualUnsupportedTypeModel
    {
        [ValGreaterThanOrEqual(1)] 
        public bool Value { get; set; }
    }

    public class GreaterThanOrEqualCustomMessageModel
    {
        [ValGreaterThanOrEqual(10, "Value must be at least ten")] 
        public int Value { get; set; }
    }

    public class GreaterThanOrEqualDefaultMessageModel
    {
        [ValGreaterThanOrEqual(10)] 
        public int Value { get; set; }
    }
}
