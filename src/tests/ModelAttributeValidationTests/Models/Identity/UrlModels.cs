#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Identity
{
    public class UrlModel
    {
        [ValUrl] 
        public string Url { get; set; }
    }

    public class UrlRequireHttpsModel
    {
        [ValUrl(true)]
        public string Url { get; set; }
    }
}
