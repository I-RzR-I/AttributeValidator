#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;

#endregion

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanNullRobustnessModel
    {
        [ValGreaterThan(0)]
        public int? NullableInt { get; set; }
    }
}
