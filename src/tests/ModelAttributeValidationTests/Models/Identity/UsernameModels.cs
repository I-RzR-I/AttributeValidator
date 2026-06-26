#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Identity
{
    public class UsernameModel
    {
        [ValUsername] 
        public string Username { get; set; }
    }

    public class UsernameCustomBoundsModel
    {
        [ValUsername(2, 5)]
        public string Username { get; set; }
    }
}
