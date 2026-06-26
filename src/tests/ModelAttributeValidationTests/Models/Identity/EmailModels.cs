#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Identity
{
    public class EmailModel
    {
        [ValEmail] 
        public string Email { get; set; }
    }

    public class EmailCustomMessageModel
    {
        [ValEmail("Custom email error")] 
        public string Email { get; set; }
    }
}
