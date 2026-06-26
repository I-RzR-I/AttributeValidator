#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Misc
{
    [TestClass]
    public class ValidationGuidNotEmptyTests
    {
        private static (bool isValid, List<ValidationResult> results) ValidateGuid(GuidNotEmptyGuidModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        private static (bool isValid, List<ValidationResult> results) ValidateNullable(GuidNotEmptyNullableGuidModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);
        
            return (isValid, results);
        }

        private static (bool isValid, List<ValidationResult> results) ValidateString(GuidNotEmptyStringModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);
            
            return (isValid, results);
        }

        [TestMethod]
        public void GuidNotEmpty_NewGuid_IsValid()
        {
            // Arrange
            var model = new GuidNotEmptyGuidModel { GId = Guid.NewGuid() };

            // Act
            var (isValid, _) = ValidateGuid(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GuidNotEmpty_GuidEmpty_IsInvalid()
        {
            // Arrange
            var model = new GuidNotEmptyGuidModel { GId = Guid.Empty };

            // Act
            var (isValid, _) = ValidateGuid(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GuidNotEmpty_NullableWithRealGuid_IsValid()
        {
            // Arrange
            var model = new GuidNotEmptyNullableGuidModel { GId = Guid.NewGuid() };

            // Act
            var (isValid, _) = ValidateNullable(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GuidNotEmpty_NullableSetToNull_IsInvalid()
        {
            // Arrange
            var model = new GuidNotEmptyNullableGuidModel { GId = null };

            // Act
            var (isValid, _) = ValidateNullable(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GuidNotEmpty_StringValidGuid_IsValid()
        {
            // Arrange
            var model = new GuidNotEmptyStringModel { GId = Guid.NewGuid().ToString() };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GuidNotEmpty_StringAllZeroes_IsInvalid()
        {
            // Arrange
            var model = new GuidNotEmptyStringModel { GId = "00000000-0000-0000-0000-000000000000" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GuidNotEmpty_StringNotAGuid_IsInvalid()
        {
            // Arrange
            var model = new GuidNotEmptyStringModel { GId = "not-a-guid" };

            // Act
            var (isValid, _) = ValidateString(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GuidNotEmpty_GuidEmpty_ErrorMessageIsNonEmpty()
        {
            // Arrange
            var model = new GuidNotEmptyGuidModel { GId = Guid.Empty };

            // Act
            var (_, results) = ValidateGuid(model);

            Assert.IsTrue(results.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(results[0].ErrorMessage));
        }

        [TestMethod]
        public void GuidNotEmpty_GuidEmpty_ErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new GuidNotEmptyGuidModel { GId = Guid.Empty };

            // Act
            var (_, results) = ValidateGuid(model);

            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "GId");
        }
    }
}
