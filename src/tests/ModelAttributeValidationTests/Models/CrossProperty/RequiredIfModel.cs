#region U S I N G

using RzR.Validation.Attributes.Attributes.Conditional;
using RzR.Validation.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.CrossProperty
{
    public class RequiredIfStringModel
    {
        public string PaymentType { get; set; }

        [ValRequiredIf(nameof(PaymentType), ValOp.Equals, "Card")]
        public string CardNumber { get; set; }
    }

    public class RequiredIfIntGreaterThanModel
    {
        public int Quantity { get; set; }

        [ValRequiredIf(nameof(Quantity), ValOp.GreaterThan, 0)] 
        public string Notes { get; set; }
    }

    public class RequiredIfCustomMessageModel
    {
        public string Mode { get; set; }

        [ValRequiredIf(nameof(Mode), ValOp.Equals, "Manual", "Mode is Manual — field is mandatory.")] 
        public string ManualValue { get; set; }
    }
}
