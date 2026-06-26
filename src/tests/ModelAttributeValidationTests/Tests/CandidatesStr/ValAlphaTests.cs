#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CandidatesStr;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CandidatesStr
{
    [TestClass]
    public class ValAlphaTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValAlpha_AllLowercase_IsValid()
        {
            // Arrange
            var model = new AlphaModel { Value = "Hello" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValAlpha_MixedCase_IsValid()
        {
            // Arrange
            var model = new AlphaModel { Value = "abcXYZ" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValAlpha_ContainsDigits_IsInvalid()
        {
            // Arrange
            var model = new AlphaModel { Value = "abc123" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValAlpha_ContainsSpace_IsInvalid()
        {
            // Arrange
            var model = new AlphaModel { Value = "ab c" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValAlpha_EmptyString_IsInvalid()
        {
            // Arrange
            var model = new AlphaModel { Value = "" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValAlpha_NullValue_IsInvalid()
        {
            // Arrange
            var model = new AlphaModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValAlpha_InvalidValue_DefaultErrorMessageContainsLetters()
        {
            // Arrange
            var model = new AlphaModel { Value = "abc123" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "letters");
        }

        [TestMethod]
        public void ValAlpha_InvalidValue_CustomUserMessageIsReturned()
        {
            // Arrange
            var model = new AlphaCustomMessageModel { Value = "bad123" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("Only ASCII letters are allowed here.", results[0].ErrorMessage);
        }
    }
}
