#region U S I N G

using RzR.Validation.Attributes.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.Misc
{
    public class DeniedValuesStringModel
    {
        [ValDeniedValues("X", "Y")] 
        public string Code { get; set; }
    }
}
