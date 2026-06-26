#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Candidates
{
    public class PostalCodeChModel
    {
        [ValPostalCode("CH")]
        public string PostalCode { get; set; }
    }

    public class PostalCodeAtModel
    {
        [ValPostalCode("AT")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeBeModel
    {
        [ValPostalCode("BE")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeSeModel
    {
        [ValPostalCode("SE")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodePtModel
    {
        [ValPostalCode("PT")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodePlModel
    {
        [ValPostalCode("PL")]
        public string PostalCode { get; set; }
    }

    public class PostalCodeBrModel
    {
        [ValPostalCode("BR")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeInModel
    {
        [ValPostalCode("IN")]
        public string PostalCode { get; set; }
    }

    public class PostalCodeRuModel
    {
        [ValPostalCode("RU")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeLtModel
    {
        [ValPostalCode("LT")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeMcModel
    {
        [ValPostalCode("MC")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeIsModel
    {
        [ValPostalCode("IS")]
        public string PostalCode { get; set; }
    }

    public class PostalCodeIeModel
    {
        [ValPostalCode("IE")]
        public string PostalCode { get; set; }
    }

    public class PostalCodeArModel
    {
        [ValPostalCode("AR")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeNlModel
    {
        [ValPostalCode("NL")] 
        public string PostalCode { get; set; }
    }

    public class PostalCodeMdModel
    {
        [ValPostalCode("MD")] 
        public string PostalCode { get; set; }
    }
}
