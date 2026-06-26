#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Identity
{
    public class CreditCardModel
    {
        [ValCreditCard] 
        public string CardNumber { get; set; }
    }

    public class CreditCardCustomMessageModel
    {
        [ValCreditCard("Custom card error")]
        public string CardNumber { get; set; }
    }
}
