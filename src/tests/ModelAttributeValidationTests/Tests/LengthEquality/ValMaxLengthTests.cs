#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.LengthEquality;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.LengthEquality
{
    [TestClass]
    public class ValMaxLengthTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(MaxLengthModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void MaxLength5_StringOf3Chars_IsValid()
        {
            // Arrange
            var model = new MaxLengthModel { Value = "abc" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MaxLength5_StringOfExactly5Chars_IsValid()
        {
            // Arrange
            var model = new MaxLengthModel { Value = "abcde" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MaxLength5_StringOf6Chars_IsInvalid()
        {
            // Arrange
            var model = new MaxLengthModel { Value = "abcdef" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MaxLength5_NullString_IsInvalid()
        {
            // Arrange
            var model = new MaxLengthModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MaxLength5_DefaultErrorMessage_ContainsAtMost()
        {
            // Arrange
            var model = new MaxLengthModel { Value = "toolongvalue" };

            // Act
            var (_, results) = Validate(model);

            // Assert
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "at most");
        }
    }
}
