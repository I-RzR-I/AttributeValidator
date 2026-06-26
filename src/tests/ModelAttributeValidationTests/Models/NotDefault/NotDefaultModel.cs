#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.NotDefault
{
    public class NotDefaultModel
    {
        [ValRequiredNotDefault]
        public int Id { get; set; }

        [ValRequiredNotDefault] 
        public string Name { get; set; }

        [ValRequiredNotDefault] 
        public string Code { get; set; }

        [ValRequiredNotDefault] 
        public bool IsActive { get; set; }
    }
}
