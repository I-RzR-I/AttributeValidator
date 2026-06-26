#region U S I N G

using RzR.Validation.Attributes.Attributes.String;

#endregion

namespace ModelAttributeValidationTests.Models.LengthEquality
{
    public class MinLengthModel
    {
        [ValMinLength(3)] 
        public string Value { get; set; }
    }
}
