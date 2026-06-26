#region U S I N G

using RzR.Validation.Attributes.Attributes.Object;

#endregion

namespace ModelAttributeValidationTests.Models.CrossProperty
{
    [ValAtLeastOneOf(nameof(Email), nameof(Phone))]
    public class ContactModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    [ValAtLeastOneOf(nameof(PrimaryEmail), nameof(SecondaryEmail))]
    public class MultiFieldContactModel
    {
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string Notes { get; set; }
    }
}
