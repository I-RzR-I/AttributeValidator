#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.LengthEquality;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.LengthEquality
{
    [TestClass]
    public class ValEqualTests
    {

        private static (bool isValid, List<ValidationResult> results) ValidateString(EqualStringModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        private static (bool isValid, List<ValidationResult> results) ValidateInt(EqualIntModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        private static (bool isValid, List<ValidationResult> results) ValidateCrossWidth(EqualCrossWidthModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void Equal_StringMatchesComparand_IsValid()
        {
            // Arrange
            var model = new EqualStringModel { Answer = "Yes" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Equal_StringDoesNotMatchComparand_IsInvalid()
        {
            // Arrange
            var model = new EqualStringModel { Answer = "No" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Equal_IntMatchesComparand_IsValid()
        {
            // Arrange
            var model = new EqualIntModel { Score = 42 };

            // Act
            var (isValid, _) = ValidateInt(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Equal_IntDoesNotMatchComparand_IsInvalid()
        {
            // Arrange
            var model = new EqualIntModel { Score = 7 };

            // Act
            var (isValid, _) = ValidateInt(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Equal_LongPropertyMatchesIntComparand_CrossWidth_IsValid()
        {
            // Arrange
            var model = new EqualCrossWidthModel { Quantity = 1L };

            // Act
            var (isValid, _) = ValidateCrossWidth(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Equal_LongPropertyDoesNotMatchIntComparand_CrossWidth_IsInvalid()
        {
            // Arrange
            var model = new EqualCrossWidthModel { Quantity = 2L };

            // Act
            var (isValid, _) = ValidateCrossWidth(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Equal_DefaultErrorMessage_ContainsMustEqual()
        {
            // Arrange
            var model = new EqualStringModel { Answer = "No" };

            // Act
            var (_, results) = ValidateString(model);

            // Assert
            Assert.IsTrue(results.Count > 0, "Expected at least one validation result.");
            StringAssert.Contains(results[0].ErrorMessage, "must equal");
        }
    }
}
