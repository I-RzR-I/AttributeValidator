#region U S I N G

using RzR.Validation.Attributes.Attributes.String;

#endregion

namespace ModelAttributeValidationTests.Models.LengthEquality
{
    public class ExactLengthModel
    {
        [ValExactLength(4)]
        public string Value { get; set; }
    }
}
