#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.NotEmpty
{
    public class NotEmptyNullableModel
    {
        [ValRequiredNotEmpty]
        public int? Id { get; set; }

        [ValRequiredNotEmpty]
        public string Name { get; set; }

        [ValRequiredNotEmpty] 
        public string Code { get; set; }

        [ValRequiredNotEmpty]
        public bool? IsActive { get; set; }

        [ValRequiredNotEmpty]
        public Guid? GId { get; set; }
    }
}
