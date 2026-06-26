#region U S I N G

using RzR.Validation.Attributes.Attributes.Format;

#endregion

namespace ModelAttributeValidationTests.Models.Domain
{

    public class IpAddressModel
    {
        [ValIpAddress] 
        public string IpAddress { get; set; }
    }

    public class HexColorModel
    {
        [ValHexColor]
        public string Color { get; set; }
    }

    public class Base64Model
    {
        [ValBase64]
        public string Payload { get; set; }
    }

    public class PhoneE164Model
    {
        [ValPhoneE164] 
        public string PhoneNumber { get; set; }
    }

    public class IbanModel
    {
        [ValIban] 
        public string Iban { get; set; }
    }
}
