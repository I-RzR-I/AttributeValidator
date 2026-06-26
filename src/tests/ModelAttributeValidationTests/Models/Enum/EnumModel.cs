#region U S I N G

using RzR.Validation.Attributes.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.Enum
{
    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Shipped = 2,
        Delivered = 3
    }

    public class EnumModel
    {
        [ValEnum(typeof(OrderStatus), "Status")] 
        public OrderStatus Status { get; set; }
    }
}
