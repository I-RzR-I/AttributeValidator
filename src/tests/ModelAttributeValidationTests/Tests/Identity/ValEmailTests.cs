#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Identity
{
    [TestClass]
    public class ValEmailTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValEmail_SimpleAddress_IsValid()
        {
            // Arrange
            var model = new EmailModel { Email = "user@example.com" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValEmail_PlusTagAndSubDomain_IsValid()
        {
            // Arrange
            var model = new EmailModel { Email = "user.name+tag@sub.example.co" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValEmail_DoubleAtSign_IsInvalid()
        {
            // Arrange
            var model = new EmailModel { Email = "user@@example" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValEmail_NoAtSign_IsInvalid()
        {
            // Arrange
            var model = new EmailModel { Email = "no-at-sign" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValEmail_NoDotInDomain_IsInvalid()
        {
            // Arrange
            var model = new EmailModel { Email = "a@b" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValEmail_NullValue_IsInvalid()
        {
            // Arrange
            var model = new EmailModel { Email = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValEmail_300CharLocalPart_IsInvalid()
        {
            // Arrange
            var localPart = new string('a', 300);
            var model = new EmailModel { Email = $"{localPart}@example.com" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValEmail_InvalidAddress_DefaultErrorMessageContainsEmailKeyword()
        {
            // Arrange
            var model = new EmailModel { Email = "not-an-email" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "email");
        }

        [TestMethod]
        public void ValEmail_InvalidAddress_CustomUserMessageIsReturned()
        {
            // Arrange
            var model = new EmailCustomMessageModel { Email = "bad" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("Custom email error", results[0].ErrorMessage);
        }
    }
}
