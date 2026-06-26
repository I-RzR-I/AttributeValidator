#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Candidates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Candidates
{
    [TestClass]
    public class ValPostalCodeTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValPostalCode_Us_FiveDigits_IsValid()
        {
            // Arrange
            var model = new PostalCodeUsModel { PostalCode = "90210" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Us_FiveDigitsPlusFour_IsValid()
        {
            // Arrange
            var model = new PostalCodeUsModel { PostalCode = "90210-1234" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Us_AllLetters_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeUsModel { PostalCode = "ABCDE" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Us_NullValue_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeUsModel { PostalCode = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ca_ValidCanadianFormat_IsValid()
        {
            // Arrange
            var model = new PostalCodeCaModel { PostalCode = "K1A 0B1" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ca_UsZipCode_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeCaModel { PostalCode = "90210" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_De_ValidGermanCode_IsValid()
        {
            // Arrange
            var model = new PostalCodeDeModel { PostalCode = "10115" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Zz_ShortAlphanumeric_IsValid()
        {
            // Arrange
            var model = new PostalCodeZzModel { PostalCode = "AB12" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Zz_EmptyString_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeZzModel { PostalCode = "" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Invalid_DefaultErrorMessageContainsPostalKeyword()
        {
            // Arrange
            var model = new PostalCodeUsModel { PostalCode = "ABCDE" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "postal");
        }

        [TestMethod]
        public void ValPostalCode_Invalid_CustomUserMessageIsReturned()
        {
            // Arrange
            var model = new PostalCodeCustomMessageModel { PostalCode = "ABCDE" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("Custom postal error", results[0].ErrorMessage);
        }
    }
}
