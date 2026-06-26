#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Misc
{
    public class GuidNotEmptyGuidModel
    {
        [ValGuidNotEmpty]
        public Guid GId { get; set; }
    }

    public class GuidNotEmptyNullableGuidModel
    {
        [ValGuidNotEmpty] 
        public Guid? GId { get; set; }
    }

    public class GuidNotEmptyStringModel
    {
        [ValGuidNotEmpty]
        public string GId { get; set; }
    }
}
