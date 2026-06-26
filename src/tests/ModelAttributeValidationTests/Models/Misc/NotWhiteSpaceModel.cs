#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.Misc
{
    public class NotWhiteSpaceModel
    {
        [ValNotWhiteSpace]
        public string Value { get; set; }
    }
}
