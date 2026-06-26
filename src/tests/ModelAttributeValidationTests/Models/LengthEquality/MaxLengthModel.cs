#region U S I N G

using RzR.Validation.Attributes.Attributes.String;

#endregion

namespace ModelAttributeValidationTests.Models.LengthEquality
{
    public class MaxLengthModel
    {
        [ValMaxLength(5)] 
        public string Value { get; set; }
    }
}
