#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Misc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Misc
{
    [TestClass]
    public class ValidationAllowedValuesTests
    {
        private static (bool isValid, List<ValidationResult> results) ValidateString(AllowedValuesStringModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        private static (bool isValid, List<ValidationResult> results) ValidateInt(AllowedValuesIntModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void AllowedValues_StringInSet_IsValid()
        {
            // Arrange
            var model = new AllowedValuesStringModel { Code = "B" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AllowedValues_StringNotInSet_IsInvalid()
        {
            // Arrange
            var model = new AllowedValuesStringModel { Code = "D" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void AllowedValues_IntInSet_IsValid()
        {
            // Arrange
            var model = new AllowedValuesIntModel { Level = 2 };

            // Act
            var (isValid, _) = ValidateInt(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AllowedValues_IntNotInSet_IsInvalid()
        {
            // Arrange
            var model = new AllowedValuesIntModel { Level = 9 };

            // Act
            var (isValid, _) = ValidateInt(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
