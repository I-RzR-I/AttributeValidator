#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CandidatesStr;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CandidatesStr
{
    [TestClass]
    public class ValAlphaNumericTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValAlphaNumeric_LettersAndDigits_IsValid()
        {
            // Arrange
            var model = new AlphaNumericModel { Value = "abc123" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValAlphaNumeric_AllUppercaseLetters_IsValid()
        {
            // Arrange
            var model = new AlphaNumericModel { Value = "ABC" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValAlphaNumeric_AllDigits_IsValid()
        {
            // Arrange
            var model = new AlphaNumericModel { Value = "123" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValAlphaNumeric_ContainsHyphen_IsInvalid()
        {
            // Arrange
            var model = new AlphaNumericModel { Value = "abc-123" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValAlphaNumeric_ContainsSpace_IsInvalid()
        {
            // Arrange
            var model = new AlphaNumericModel { Value = "ab c" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValAlphaNumeric_EmptyString_IsInvalid()
        {
            // Arrange
            var model = new AlphaNumericModel { Value = "" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValAlphaNumeric_NullValue_IsInvalid()
        {
            // Arrange
            var model = new AlphaNumericModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
