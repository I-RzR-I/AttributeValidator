#region U S I N G

using RzR.Validation.Attributes.Attributes.Object;

#endregion

namespace ModelAttributeValidationTests.Models.CrossProperty
{
    [ValExactlyOneOf(nameof(EmailNotification), nameof(SmsNotification), nameof(PushNotification))]
    public class NotificationChannelModel
    {
        public string EmailNotification { get; set; }
        public string SmsNotification { get; set; }
        public string PushNotification { get; set; }
    }
}
