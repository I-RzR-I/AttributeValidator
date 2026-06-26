#region U S I N G

using RzR.Validation.Attributes.Attributes.Format;
using RzR.Validation.Attributes.Attributes.String;

#endregion

namespace ModelAttributeValidationTests.Models.CandidatesStr
{

    public class AlphaModel
    {
        [ValAlpha] 
        public string Value { get; set; }
    }

    public class AlphaCustomMessageModel
    {
        [ValAlpha("Only ASCII letters are allowed here.")] 
        public string Value { get; set; }
    }

    public class AlphaNumericModel
    {
        [ValAlphaNumeric]
        public string Value { get; set; }
    }

    public class NumericStringModel
    {
        [ValNumericString] 
        public string Value { get; set; }
    }

    public class ColorNameModel
    {
        [ValColorName] 
        public string Value { get; set; }
    }

    public class ColorNameCustomMessageModel
    {
        [ValColorName("Please provide a valid CSS color name.")] 
        public string Value { get; set; }
    }
}
