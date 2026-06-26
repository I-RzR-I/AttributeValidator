#region U S I N G

using RzR.Validation.Attributes.Attributes.Conditional;
using RzR.Validation.Attributes.Common;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.CrossProperty
{
    public class DateRangeModel
    {
        public DateTime StartDate { get; set; }

        [ValCompareProperty(nameof(StartDate), ValOp.GreaterThan)] 
        public DateTime EndDate { get; set; }
    }

    public class DateRangeOrEqualModel
    {
        public DateTime StartDate { get; set; }

        [ValCompareProperty(nameof(StartDate), ValOp.GreaterThanOrEqual)]
        public DateTime EndDate { get; set; }
    }

    public class PasswordConfirmModel
    {
        public string Password { get; set; }

        [ValCompareProperty(nameof(Password), ValOp.Equals)]
        public string ConfirmPassword { get; set; }
    }

    public class ComparePropertyCustomMessageModel
    {
        public int Min { get; set; }

        [ValCompareProperty(nameof(Min), ValOp.GreaterThan, "Max must exceed Min.")] 
        public int Max { get; set; }
    }
}
