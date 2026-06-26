#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Ajax;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Ajax
{
    [TestClass]
    public class ValidationAjaxOnlyTests
    {
        [TestMethod]
        public void AjaxOnly_IsAjaxTrue_IsValid()
        {
            // Arrange
            var model = new AjaxModel { IsAjax = true };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AjaxOnly_IsAjaxFalse_IsInvalid()
        {
            // Arrange
            var model = new AjaxModel { IsAjax = false };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void AjaxOnly_IsAjaxFalse_ErrorMessageIsNotEmpty()
        {
            // Arrange
            var model = new AjaxModel { IsAjax = false };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResults[0].ErrorMessage));
        }

        [TestMethod]
        public void AjaxOnly_DefaultModel_IsInvalid()
        {
            // Arrange
            var model = new AjaxModel();
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
