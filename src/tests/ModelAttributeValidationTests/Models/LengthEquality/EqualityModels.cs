#region U S I N G

using RzR.Validation.Attributes.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.LengthEquality
{
    public class EqualStringModel
    {
        [ValEqual("Yes")]
        public string Answer { get; set; }
    }

    public class EqualIntModel
    {
        [ValEqual(42)]
        public int Score { get; set; }
    }

    public class EqualCrossWidthModel
    {
        [ValEqual(1)] 
        public long Quantity { get; set; }
    }

    public class NotEqualStringModel
    {
        [ValNotEqual("admin")] 
        public string Username { get; set; }
    }

    public class NotEqualIntModel
    {
        [ValNotEqual(0)] 
        public int Count { get; set; }
    }
}
