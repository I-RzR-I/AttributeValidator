#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.LengthEquality;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.LengthEquality
{
    [TestClass]
    public class ValMinLengthTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(MinLengthModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void MinLength3_StringOfExactly3Chars_IsValid()
        {
            // Arrange
            var model = new MinLengthModel { Value = "abc" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MinLength3_StringOf2Chars_IsInvalid()
        {
            // Arrange
            var model = new MinLengthModel { Value = "ab" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MinLength3_NullString_IsInvalid()
        {
            // Arrange
            var model = new MinLengthModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
