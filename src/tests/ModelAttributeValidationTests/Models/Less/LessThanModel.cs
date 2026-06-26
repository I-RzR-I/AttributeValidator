#region U S I N G

using RzR.Validation.Attributes.Attributes.Less;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Less
{
    public class LessThanIntModel
    {
        [ValLessThan(10)] 
        public int Value { get; set; }
    }

    public class LessThanDecimalModel
    {
        [ValLessThan(5.5)] 
        public decimal Value { get; set; }
    }

    public class LessThanDoubleModel
    {
        [ValLessThan(3.14)] 
        public double Value { get; set; }
    }

    public class LessThanDateTimeModel
    {
        [ValLessThan("2025-01-01")]
        public DateTime Value { get; set; }
    }

    public class LessThanNullableIntModel
    {
        [ValLessThan(10)] 
        public int? Value { get; set; }
    }

    public class LessThanUnsupportedTypeModel
    {
        [ValLessThan(1)] 
        public bool Value { get; set; }
    }

    public class LessThanCustomMessageModel
    {
        [ValLessThan(10, "Value must be less than ten")] 
        public int Value { get; set; }
    }

    public class LessThanDefaultMessageModel
    {
        [ValLessThan(10)]
        public int Value { get; set; }
    }
}
