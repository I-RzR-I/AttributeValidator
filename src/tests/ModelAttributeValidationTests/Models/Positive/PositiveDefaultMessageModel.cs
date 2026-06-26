#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.Positive
{
    public class PositiveDefaultMessageModel
    {
        [ValRequiredPositive] 
        public int IntNumber { get; set; }
    }
}
