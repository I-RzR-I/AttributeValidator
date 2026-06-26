#region U S I N G

using RzR.Validation.Attributes.Attributes.Require;

#endregion

namespace ModelAttributeValidationTests.Models.Positive
{
    public class PositiveUnsignedModel
    {
        [ValRequiredPositive] 
        public uint UIntNumber { get; set; }

        [ValRequiredPositive] 
        public byte ByteNumber { get; set; }
    }
}
