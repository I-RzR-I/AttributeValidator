#region U S I N G

using RzR.Validation.Attributes.Attributes.Object;

#endregion

namespace ModelAttributeValidationTests.Models.CrossProperty
{
    [ValMutuallyExclusive(nameof(CreditCard), nameof(BankTransfer))]
    public class PaymentMethodModel
    {
        public string CreditCard { get; set; }
        public string BankTransfer { get; set; }
    }

    [ValMutuallyExclusive(nameof(OptionA), nameof(OptionB), nameof(OptionC))]
    public class ThreeOptionModel
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
    }
}
