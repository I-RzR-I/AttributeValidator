#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Identity
{
    public class CountryCodeModel
    {
        [ValCountryCode]
        public string Code { get; set; }
    }
}
