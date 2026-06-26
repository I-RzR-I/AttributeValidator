#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Misc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Misc
{
    [TestClass]
    public class ValidationNotWhiteSpaceTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(NotWhiteSpaceModel model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void NotWhiteSpace_NonEmptyString_IsValid()
        {
            // Arrange
            var model = new NotWhiteSpaceModel { Value = "x" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotWhiteSpace_EmptyString_IsInvalid()
        {
            // Arrange
            var model = new NotWhiteSpaceModel { Value = "" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotWhiteSpace_SpacesOnly_IsInvalid()
        {
            // Arrange
            var model = new NotWhiteSpaceModel { Value = "   " };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotWhiteSpace_TabOnly_IsInvalid()
        {
            // Arrange
            var model = new NotWhiteSpaceModel { Value = "\t" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotWhiteSpace_NullString_IsInvalid()
        {
            // Arrange
            var model = new NotWhiteSpaceModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotWhiteSpace_EmptyString_ErrorMessageIsNonEmpty()
        {
            // Arrange
            var model = new NotWhiteSpaceModel { Value = "" };

            // Act
            var (_, results) = Validate(model);

            Assert.IsTrue(results.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(results[0].ErrorMessage));
        }

        [TestMethod]
        public void NotWhiteSpace_EmptyString_ErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new NotWhiteSpaceModel { Value = "" };

            // Act
            var (_, results) = Validate(model);

            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "Value");
        }
    }
}
