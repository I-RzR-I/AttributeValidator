#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Candidates
{
    public class PostalCodeUsModel
    {
        [ValPostalCode] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeCaModel
    {
        [ValPostalCode("CA")]
        public string PostalCode { get; set; }
    }

    public class PostalCodeDeModel
    {
        [ValPostalCode("DE")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeZzModel
    {
        [ValPostalCode("ZZ")]
        public string PostalCode { get; set; }
    }

    public class PostalCodeCustomMessageModel
    {
        [ValPostalCode("US", "Custom postal error")] 
        public string PostalCode { get; set; }
    }
}
