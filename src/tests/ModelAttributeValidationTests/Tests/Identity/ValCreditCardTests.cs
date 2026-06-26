#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Identity
{
    [TestClass]
    public class ValCreditCardTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValCreditCard_ValidLuhn16Digits_IsValid()
        {
            // Arrange
            var model = new CreditCardModel { CardNumber = "4111111111111111" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCreditCard_ValidLuhnWithSpaces_IsValid()
        {
            // Arrange
            var model = new CreditCardModel { CardNumber = "4111 1111 1111 1111" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCreditCard_BadLuhnChecksum_IsInvalid()
        {
            // Arrange
            var model = new CreditCardModel { CardNumber = "4111111111111112" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCreditCard_TooShort_IsInvalid()
        {
            // Arrange
            var model = new CreditCardModel { CardNumber = "1234" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCreditCard_NonNumericString_IsInvalid()
        {
            // Arrange
            var model = new CreditCardModel { CardNumber = "notacard" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCreditCard_NullValue_IsInvalid()
        {
            // Arrange
            var model = new CreditCardModel { CardNumber = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCreditCard_Invalid_DefaultErrorMessageContainsCreditCardKeyword()
        {
            // Arrange
            var model = new CreditCardModel { CardNumber = "1234" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "credit card");
        }

        [TestMethod]
        public void ValCreditCard_Invalid_CustomUserMessageIsReturned()
        {
            // Arrange
            var model = new CreditCardCustomMessageModel { CardNumber = "1234" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("Custom card error", results[0].ErrorMessage);
        }
    }
}
