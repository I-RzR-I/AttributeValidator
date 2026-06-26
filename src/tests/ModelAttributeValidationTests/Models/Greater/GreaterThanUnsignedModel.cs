#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;

#endregion

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanUnsignedModel
    {
        [ValGreaterThan(0u)] 
        public uint UIntNumber { get; set; }

        [ValGreaterThan((byte)0)]
        public byte ByteNumber { get; set; }
    }
}
