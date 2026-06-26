#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CrossProperty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CrossProperty
{
    [TestClass]
    public class ValidationRequiredUnlessTests
    {

        [TestMethod]
        public void RequiredUnless_StringEquals_ConditionMet_FieldNull_IsValid()
        {
            var model = new RequiredUnlessStringModel { PaymentType = "Free", InvoiceNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredUnless_StringEquals_ConditionMet_FieldSet_IsValid()
        {
            var model = new RequiredUnlessStringModel { PaymentType = "Free", InvoiceNumber = "INV-001" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredUnless_StringEquals_ConditionNotMet_FieldNull_IsInvalid()
        {
            var model = new RequiredUnlessStringModel { PaymentType = "Paid", InvoiceNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void RequiredUnless_StringEquals_ConditionNotMet_FieldSet_IsValid()
        {
            var model = new RequiredUnlessStringModel { PaymentType = "Paid", InvoiceNumber = "INV-002" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredUnless_StringEquals_OtherPropertyNull_FieldNull_IsInvalid()
        {
            var model = new RequiredUnlessStringModel { PaymentType = null, InvoiceNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void RequiredUnless_IntLessOrEqual_ConditionMet_Zero_FieldNull_IsValid()
        {
            var model = new RequiredUnlessIntModel { Priority = 0, ShippingAddress = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredUnless_IntLessOrEqual_ConditionMet_Negative_FieldNull_IsValid()
        {
            var model = new RequiredUnlessIntModel { Priority = -5, ShippingAddress = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredUnless_IntLessOrEqual_ConditionNotMet_Positive_FieldNull_IsInvalid()
        {
            var model = new RequiredUnlessIntModel { Priority = 3, ShippingAddress = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void RequiredUnless_IntLessOrEqual_ConditionNotMet_Positive_FieldSet_IsValid()
        {
            var model = new RequiredUnlessIntModel { Priority = 3, ShippingAddress = "123 Main St" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredUnless_CustomMessage_ConditionNotMet_FieldNull_ReturnsCustomMessage()
        {
            var model = new RequiredUnlessCustomMessageModel { Channel = "SMS", PhoneNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual("Phone required unless channel is Email.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void RequiredUnless_CustomMessage_ConditionMet_IsValid()
        {
            var model = new RequiredUnlessCustomMessageModel { Channel = "Email", PhoneNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }
    }
}
