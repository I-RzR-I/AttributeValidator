#region U S I N G

using RzR.Validation.Attributes.Attributes.Identity;

#endregion

namespace ModelAttributeValidationTests.Models.Candidates
{
    public class SlugModel
    {
        [ValSlug] 
        public string Slug { get; set; }
    }

    public class SlugCustomMessageModel
    {
        [ValSlug("Custom slug error")] 
        public string Slug { get; set; }
    }
}
