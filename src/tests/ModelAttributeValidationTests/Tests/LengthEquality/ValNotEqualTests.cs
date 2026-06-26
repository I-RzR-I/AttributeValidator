#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.LengthEquality;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.LengthEquality
{
    [TestClass]
    public class ValNotEqualTests
    {
        private static (bool isValid, List<ValidationResult> results) ValidateString(NotEqualStringModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);
        
            return (isValid, results);
        }

        private static (bool isValid, List<ValidationResult> results) ValidateInt(NotEqualIntModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);
            
            return (isValid, results);
        }

        [TestMethod]
        public void NotEqual_StringDiffersFromComparand_IsValid()
        {
            // Arrange
            var model = new NotEqualStringModel { Username = "user" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotEqual_StringMatchesComparand_IsInvalid()
        {
            // Arrange
            var model = new NotEqualStringModel { Username = "admin" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotEqual_IntDiffersFromComparand_IsValid()
        {
            // Arrange
            var model = new NotEqualIntModel { Count = 5 };

            // Act
            var (isValid, _) = ValidateInt(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotEqual_IntMatchesComparand_IsInvalid()
        {
            // Arrange
            var model = new NotEqualIntModel { Count = 0 };

            // Act
            var (isValid, _) = ValidateInt(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
