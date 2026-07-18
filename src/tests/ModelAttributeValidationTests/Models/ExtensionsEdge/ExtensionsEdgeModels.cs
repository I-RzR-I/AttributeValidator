#region U S I N G

using RzR.Validation.Attributes.Attributes.Conditional;
using RzR.Validation.Attributes.Attributes.Object;
using RzR.Validation.Attributes.Attributes.Require;
using RzR.Validation.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.ExtensionsEdge
{
    public class ValidSimpleModel
    {
        [ValRequiredNotNull]
        public string Name { get; set; }
    }

    public class InvalidSimpleModel
    {
        [ValRequiredNotNull]
        public string Name { get; set; }
    }

    [ValAtLeastOneOf(nameof(Email), nameof(Phone))]
    public class AtLeastOneContactEdgeModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class RequiredIfEdgeModel
    {
        public string PaymentType { get; set; }

        [ValRequiredIf(nameof(PaymentType), ValOp.Equals, "Card")]
        public string CardNumber { get; set; }
    }
}
