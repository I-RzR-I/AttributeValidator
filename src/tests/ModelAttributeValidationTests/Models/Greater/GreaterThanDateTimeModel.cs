#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanDateTimeModel
    {
        [ValGreaterThan("2024-01-01")] 
        public DateTime Date { get; set; }
    }

    public class GreaterThanDateTimeEqualModel
    {
        [ValGreaterThan("2024-06-15")]
        public DateTime Date { get; set; }
    }

    public class GreaterThanNullableDateTimeModel
    {
        [ValGreaterThan("2024-01-01")]
        public DateTime? Date { get; set; }
    }
}
