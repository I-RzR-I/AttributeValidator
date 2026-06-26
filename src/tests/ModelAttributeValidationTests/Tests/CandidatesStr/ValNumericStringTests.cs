#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CandidatesStr;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CandidatesStr
{
    [TestClass]
    public class ValNumericStringTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValNumericString_DigitsOnly_IsValid()
        {
            // Arrange
            var model = new NumericStringModel { Value = "12345" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValNumericString_LeadingZeros_IsValid()
        {
            // Arrange
            var model = new NumericStringModel { Value = "007" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValNumericString_ContainsDot_IsInvalid()
        {
            // Arrange
            var model = new NumericStringModel { Value = "12.3" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValNumericString_ContainsLetter_IsInvalid()
        {
            // Arrange
            var model = new NumericStringModel { Value = "12a" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValNumericString_EmptyString_IsInvalid()
        {
            // Arrange
            var model = new NumericStringModel { Value = "" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValNumericString_NullValue_IsInvalid()
        {
            // Arrange
            var model = new NumericStringModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
