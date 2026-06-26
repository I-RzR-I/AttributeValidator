#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.NotDefault
{
    public class NotDefaultWithGuidModel
    {
        [ValRequiredNotDefault]
        public int Id { get; set; }

        [ValRequiredNotDefault] 
        public string Name { get; set; }

        [ValRequiredNotDefault] 
        public bool IsActive { get; set; }

        [ValRequiredNotDefault]
        public Guid GId { get; set; }
    }
}
