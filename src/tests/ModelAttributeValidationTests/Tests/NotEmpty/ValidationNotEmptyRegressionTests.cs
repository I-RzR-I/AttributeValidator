#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Validation.Attributes.Attributes.Require;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.NotEmpty
{
    internal class StringOnlyModel
    {
        [ValRequiredNotEmpty] public string Value { get; set; }
    }

    [TestClass]
    public class ValidationNotEmptyRegressionTests
    {

        [TestMethod]
        public void NotEmpty_Regression_NullString_IsInvalid()
        {
            // Arrange
            var model = new StringOnlyModel { Value = null };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotEmpty_Regression_NullString_ErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new StringOnlyModel { Value = null };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(validationResults.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResults[0].ErrorMessage));
            StringAssert.Contains(validationResults[0].ErrorMessage, "Value");
        }

        [TestMethod]
        public void NotEmpty_Regression_WhitespaceString_IsInvalid()
        {
            // Arrange
            var model = new StringOnlyModel { Value = "   " };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotEmpty_Regression_WhitespaceString_ErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new StringOnlyModel { Value = "   " };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(validationResults.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResults[0].ErrorMessage));
            StringAssert.Contains(validationResults[0].ErrorMessage, "Value");
        }

        [TestMethod]
        public void NotEmpty_Regression_NonEmptyString_IsValid()
        {
            // Arrange
            var model = new StringOnlyModel { Value = "x" };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }
    }
}
