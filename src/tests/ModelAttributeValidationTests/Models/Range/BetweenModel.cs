#region U S I N G

using RzR.Validation.Attributes.Attributes.Range;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Range
{
    public class BetweenInclusiveIntModel
    {
        [ValBetween(1, 10)]
        public int Value { get; set; }
    }

    public class BetweenExclusiveIntModel
    {
        [ValBetween(1, 10, false)]
        public int Value { get; set; }
    }

    public class BetweenDecimalModel
    {
        [ValBetween(1.0, 5.0)]
        public decimal Value { get; set; }
    }

    public class BetweenDateTimeModel
    {
        [ValBetween("2024-01-01", "2025-01-01")] 
        public DateTime Value { get; set; }
    }

    public class BetweenNullableIntModel
    {
        [ValBetween(1, 10)] 
        public int? Value { get; set; }
    }

    public class BetweenUnsupportedTypeModel
    {
        [ValBetween(0, 1)] 
        public bool Value { get; set; }
    }

    public class BetweenCustomMessageModel
    {
        [ValBetween(1, 10, true, "Value must be between 1 and 10")] 
        public int Value { get; set; }
    }

    public class BetweenDefaultMessageModel
    {
        [ValBetween(1, 10)] 
        public int Value { get; set; }
    }
}
