#region U S I N G

using RzR.Validation.Attributes.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.Misc
{
    public class AllowedValuesStringModel
    {
        [ValAllowedValues("A", "B", "C")]
        public string Code { get; set; }
    }

    public class AllowedValuesIntModel
    {
        [ValAllowedValues(1, 2, 3)]
        public int Level { get; set; }
    }
}
