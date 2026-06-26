#region U S I N G

using RzR.Validation.Attributes.Attributes.Less;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Less
{
    public class LessThanOrEqualIntModel
    {
        [ValLessThanOrEqual(10)]
        public int Value { get; set; }
    }

    public class LessThanOrEqualDecimalModel
    {
        [ValLessThanOrEqual(5.5)]
        public decimal Value { get; set; }
    }

    public class LessThanOrEqualDoubleModel
    {
        [ValLessThanOrEqual(3.14)]
        public double Value { get; set; }
    }

    public class LessThanOrEqualDateTimeModel
    {
        [ValLessThanOrEqual("2025-01-01")]
        public DateTime Value { get; set; }
    }

    public class LessThanOrEqualNullableIntModel
    {
        [ValLessThanOrEqual(10)] 
        public int? Value { get; set; }
    }

    public class LessThanOrEqualUnsupportedTypeModel
    {
        [ValLessThanOrEqual(1)] 
        public bool Value { get; set; }
    }

    public class LessThanOrEqualCustomMessageModel
    {
        [ValLessThanOrEqual(10, "Value must be at most ten")] 
        public int Value { get; set; }
    }

    public class LessThanOrEqualDefaultMessageModel
    {
        [ValLessThanOrEqual(10)] 
        public int Value { get; set; }
    }
}
