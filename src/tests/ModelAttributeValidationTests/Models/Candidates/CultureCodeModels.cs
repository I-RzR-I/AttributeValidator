#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Candidates
{
    public class CultureCodeModel
    {
        [ValCultureCode] 
        public string Culture { get; set; }
    }

    public class CultureCodeCustomMessageModel
    {
        [ValCultureCode("Custom culture error")]
        public string Culture { get; set; }
    }
}
