#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Misc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Misc
{
    [TestClass]
    public class ValidationDeniedValuesTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(DeniedValuesStringModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void DeniedValues_StringNotInDeniedSet_IsValid()
        {
            // Arrange
            var model = new DeniedValuesStringModel { Code = "Z" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void DeniedValues_StringInDeniedSet_IsInvalid()
        {
            // Arrange
            var model = new DeniedValuesStringModel { Code = "X" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void DeniedValues_SecondDeniedValue_IsInvalid()
        {
            // Arrange
            var model = new DeniedValuesStringModel { Code = "Y" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
