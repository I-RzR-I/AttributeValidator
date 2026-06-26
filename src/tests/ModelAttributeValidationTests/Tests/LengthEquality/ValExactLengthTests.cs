#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.LengthEquality;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.LengthEquality
{
    [TestClass]
    public class ValExactLengthTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(ExactLengthModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ExactLength4_StringOfExactly4Chars_IsValid()
        {
            // Arrange
            var model = new ExactLengthModel { Value = "abcd" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ExactLength4_StringOf3Chars_IsInvalid()
        {
            // Arrange
            var model = new ExactLengthModel { Value = "abc" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ExactLength4_StringOf5Chars_IsInvalid()
        {
            // Arrange
            var model = new ExactLengthModel { Value = "abcde" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ExactLength4_NullString_IsInvalid()
        {
            // Arrange
            var model = new ExactLengthModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
