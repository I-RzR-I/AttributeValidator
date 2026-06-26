#region U S I N G

using RzR.Validation.Attributes.Attributes.Conditional;
using RzR.Validation.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.CrossProperty
{
    public class RequiredUnlessStringModel
    {
        public string PaymentType { get; set; }

        [ValRequiredUnless(nameof(PaymentType), ValOp.Equals, "Free")] 
        public string InvoiceNumber { get; set; }
    }

    public class RequiredUnlessIntModel
    {
        public int Priority { get; set; }

        [ValRequiredUnless(nameof(Priority), ValOp.LessThanOrEqual, 0)] 
        public string ShippingAddress { get; set; }
    }

    public class RequiredUnlessCustomMessageModel
    {
        public string Channel { get; set; }

        [ValRequiredUnless(nameof(Channel), ValOp.Equals, "Email", "Phone required unless channel is Email.")] 
        public string PhoneNumber { get; set; }
    }
}
