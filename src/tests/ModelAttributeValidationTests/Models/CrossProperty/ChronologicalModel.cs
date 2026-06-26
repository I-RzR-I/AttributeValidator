#region U S I N G

using RzR.Validation.Attributes.Attributes.Object;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.CrossProperty
{
    [ValChronological(nameof(Created), nameof(Approved), nameof(Shipped))]
    public class OrderLifecycleModel
    {
        public DateTime? Created { get; set; }
        public DateTime? Approved { get; set; }
        public DateTime? Shipped { get; set; }
    }

    [ValChronological(nameof(Start), nameof(End))]
    public class DateIntervalModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
