#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.Extensions
{
    public class ValidationExtensionsModel
    {
        [ValRequiredNotEmpty]
        public string Name { get; set; }

        [ValGreaterThan(0)]
        public int Quantity { get; set; }
    }
}
