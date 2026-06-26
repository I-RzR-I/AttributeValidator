#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CrossProperty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CrossProperty
{
    [TestClass]
    public class ValidationMutuallyExclusiveTests
    {

        [TestMethod]
        public void MutuallyExclusive_BothSet_IsInvalid()
        {
            var model = new PaymentMethodModel { CreditCard = "4111111111111111", BankTransfer = "IBAN123" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void MutuallyExclusive_OnlyCreditCardSet_IsValid()
        {
            var model = new PaymentMethodModel { CreditCard = "4111111111111111", BankTransfer = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MutuallyExclusive_OnlyBankTransferSet_IsValid()
        {
            var model = new PaymentMethodModel { CreditCard = null, BankTransfer = "IBAN123" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MutuallyExclusive_NoneSet_IsValid()
        {
            var model = new PaymentMethodModel { CreditCard = null, BankTransfer = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MutuallyExclusive_WhitespaceNotCountedAsPopulated_IsValid()
        {
            var model = new PaymentMethodModel { CreditCard = "4111111111111111", BankTransfer = "   " };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MutuallyExclusive_DefaultErrorMessage_ContainsFieldNames()
        {
            var model = new PaymentMethodModel { CreditCard = "4111111111111111", BankTransfer = "IBAN123" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ErrorMessage.Contains("CreditCard"));
            Assert.IsTrue(results[0].ErrorMessage.Contains("BankTransfer"));
        }

        [TestMethod]
        public void MutuallyExclusive_ThreeFields_TwoSet_IsInvalid()
        {
            var model = new ThreeOptionModel { OptionA = "A", OptionB = "B", OptionC = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MutuallyExclusive_ThreeFields_AllThreeSet_IsInvalid()
        {
            var model = new ThreeOptionModel { OptionA = "A", OptionB = "B", OptionC = "C" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MutuallyExclusive_ThreeFields_OnlyOneSet_IsValid()
        {
            var model = new ThreeOptionModel { OptionA = null, OptionB = "B", OptionC = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }
    }
}
