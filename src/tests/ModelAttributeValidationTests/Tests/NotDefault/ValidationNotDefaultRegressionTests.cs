#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.NotDefault;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.NotDefault
{
    [TestClass]
    public class ValidationNotDefaultRegressionTests
    {
        [TestMethod]
        public void NotDefault_Regression_IntZero_IsInvalid()
        {
            // Arrange
            var model = new NotDefaultWithGuidModel { Id = 0, Name = "Name1", IsActive = true, GId = Guid.NewGuid() };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotDefault_Regression_IntZero_ErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new NotDefaultWithGuidModel { Id = 0, Name = "Name1", IsActive = true, GId = Guid.NewGuid() };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(validationResults.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResults[0].ErrorMessage));
            StringAssert.Contains(validationResults[0].ErrorMessage, "Id");
        }

        [TestMethod]
        public void NotDefault_Regression_BoolFalse_IsInvalid()
        {
            // Arrange
            var model = new NotDefaultWithGuidModel { Id = 1, Name = "Name1", IsActive = false, GId = Guid.NewGuid() };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotDefault_Regression_BoolFalse_ErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new NotDefaultWithGuidModel { Id = 1, Name = "Name1", IsActive = false, GId = Guid.NewGuid() };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(validationResults.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResults[0].ErrorMessage));
            StringAssert.Contains(validationResults[0].ErrorMessage, "IsActive");
        }

        [TestMethod]
        public void NotDefault_Regression_GuidEmpty_IsInvalid()
        {
            // Arrange
            var model = new NotDefaultWithGuidModel { Id = 1, Name = "Name1", IsActive = true, GId = Guid.Empty };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotDefault_Regression_AllNonDefault_IsValid()
        {
            // Arrange
            var model = new NotDefaultWithGuidModel { Id = 42, Name = "RegressionName", IsActive = true, GId = Guid.NewGuid() };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
