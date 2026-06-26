#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CrossProperty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CrossProperty
{
    [TestClass]
    public class ValidationRequiredIfTests
    {

        [TestMethod]
        public void RequiredIf_StringEquals_ConditionMet_FieldNull_IsInvalid()
        {
            var model = new RequiredIfStringModel { PaymentType = "Card", CardNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void RequiredIf_StringEquals_ConditionMet_FieldSet_IsValid()
        {
            var model = new RequiredIfStringModel { PaymentType = "Card", CardNumber = "4111111111111111" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredIf_StringEquals_ConditionNotMet_FieldNull_IsValid()
        {
            var model = new RequiredIfStringModel { PaymentType = "Cash", CardNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredIf_StringEquals_ConditionNotMet_FieldSet_IsValid()
        {
            var model = new RequiredIfStringModel { PaymentType = "Cash", CardNumber = "4111111111111111" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredIf_StringEquals_BothNull_IsValid()
        {
            var model = new RequiredIfStringModel { PaymentType = null, CardNumber = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredIf_IntGreaterThan_ConditionMet_FieldNull_IsInvalid()
        {
            var model = new RequiredIfIntGreaterThanModel { Quantity = 5, Notes = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void RequiredIf_IntGreaterThan_ConditionMet_FieldSet_IsValid()
        {
            var model = new RequiredIfIntGreaterThanModel { Quantity = 5, Notes = "Some note" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredIf_IntGreaterThan_BoundaryEqualToZero_IsValid()
        {
            var model = new RequiredIfIntGreaterThanModel { Quantity = 0, Notes = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredIf_IntGreaterThan_NegativeQuantity_IsValid()
        {
            var model = new RequiredIfIntGreaterThanModel { Quantity = -1, Notes = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void RequiredIf_CustomMessage_ConditionMet_FieldNull_ReturnsCustomMessage()
        {
            var model = new RequiredIfCustomMessageModel { Mode = "Manual", ManualValue = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual("Mode is Manual — field is mandatory.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void RequiredIf_CustomMessage_ConditionNotMet_IsValid()
        {
            var model = new RequiredIfCustomMessageModel { Mode = "Auto", ManualValue = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }
    }
}
