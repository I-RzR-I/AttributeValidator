#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CrossProperty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CrossProperty
{
    [TestClass]
    public class ValidationExactlyOneOfTests
    {

        [TestMethod]
        public void ExactlyOneOf_NoneSet_IsInvalid()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = null,
                SmsNotification = null, 
                PushNotification = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void ExactlyOneOf_OnlyEmailSet_IsValid()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = "user@example.com",
                SmsNotification = null, 
                PushNotification = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ExactlyOneOf_OnlySmsSet_IsValid()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = null,
                SmsNotification = "+1-555-0100",
                PushNotification = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ExactlyOneOf_OnlyPushSet_IsValid()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = null, 
                SmsNotification = null, 
                PushNotification = "device-token-abc"
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ExactlyOneOf_TwoSet_IsInvalid()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = "user@example.com",
                SmsNotification = "+1-555-0100",
                PushNotification = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ExactlyOneOf_AllThreeSet_IsInvalid()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = "user@example.com",
                SmsNotification = "+1-555-0100", 
                PushNotification = "device-token-abc"
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ExactlyOneOf_DefaultErrorMessage_ContainsFieldNames()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = null, 
                SmsNotification = null, 
                PushNotification = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ErrorMessage.Contains("EmailNotification"));
        }

        [TestMethod]
        public void ExactlyOneOf_WhitespaceNotCountedAsSet_IsInvalid()
        {
            var model = new NotificationChannelModel
            {
                EmailNotification = "   ", 
                SmsNotification = null, 
                PushNotification = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }
    }
}
