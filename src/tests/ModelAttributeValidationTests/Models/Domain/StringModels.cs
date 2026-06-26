#region U S I N G

using RzR.Validation.Attributes.Attributes.String;

#endregion

namespace ModelAttributeValidationTests.Models.Domain
{

    public class LengthRangeModel
    {
        [ValLengthRange(2, 5)]
        public string Code { get; set; }
    }

    public class StartsWithModel
    {
        [ValStartsWith("INV-")]
        public string InvoiceNumber { get; set; }
    }

    public class StartsWithIgnoreCaseModel
    {
        [ValStartsWith("INV-", true)] 
        public string InvoiceNumber { get; set; }
    }

    public class EndsWithModel
    {
        [ValEndsWith(".pdf")] 
        public string FileName { get; set; }
    }

    public class ContainsModel
    {
        [ValContains("@")] 
        public string Email { get; set; }
    }

    public class RegexModel
    {
        [ValRegex(@"^\d{3}$")]
        public string Code { get; set; }
    }

    public class RegexBadPatternModel
    {
        [ValRegex(@"[invalid_regex(")]
        public string Value { get; set; }
    }
}
