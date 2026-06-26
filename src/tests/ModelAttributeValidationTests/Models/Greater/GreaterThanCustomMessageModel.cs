#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;

#endregion

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanCustomMessageModel
    {
        [ValGreaterThan(10, "Value must exceed ten")] 
        public int IntWithCustomMessage { get; set; }
    }

    public class GreaterThanDefaultMessageModel
    {
        [ValGreaterThan(10)] 
        public int IntWithDefaultMessage { get; set; }
    }
}
