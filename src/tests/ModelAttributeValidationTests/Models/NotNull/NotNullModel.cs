#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.NotNull
{
    public class NotNullModel
    {
        [ValRequiredNotNull]
        public int Id { get; set; }

        [ValRequiredNotNull]
        public string Name { get; set; }

        [ValRequiredNotNull] 
        public string Code { get; set; }

        [ValRequiredNotNull] 
        public bool IsActive { get; set; }
    }
}
