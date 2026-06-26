#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Less;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Less
{
    [TestClass]
    public class ValidationLessThanOrEqualTests
    {
        [TestMethod]
        public void LessThanOrEqual_Int_ValueLessThanThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanOrEqualIntModel { Value = 3 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_Int_ValueGreaterThanThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanOrEqualIntModel { Value = 15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_Int_ValueEqualToThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanOrEqualIntModel { Value = 10 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_Decimal_FractionalValueBelowThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanOrEqualDecimalModel { Value = 5.4m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_Decimal_FractionalValueAboveThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanOrEqualDecimalModel { Value = 5.6m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_Double_FractionalValueBelowThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanOrEqualDoubleModel { Value = 3.13 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_Double_FractionalValueAboveThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanOrEqualDoubleModel { Value = 3.15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_DateTime_ValueBeforeThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanOrEqualDateTimeModel { Value = new DateTime(2024, 6, 1) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_DateTime_ValueAfterThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanOrEqualDateTimeModel { Value = new DateTime(2026, 1, 1) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_NullValue_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new LessThanOrEqualNullableIntModel { Value = null };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = false;
            Exception caughtException = null;
            try
            {
                isValid = Validator.TryValidateObject(model, context, results, true);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.IsNull(caughtException);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_UnsupportedType_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new LessThanOrEqualUnsupportedTypeModel { Value = false };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = false;
            Exception caughtException = null;
            try
            {
                isValid = Validator.TryValidateObject(model, context, results, true);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.IsNull(caughtException);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThanOrEqual_CustomMessage_FailureReturnsCustomMessage()
        {
            // Arrange
            var model = new LessThanOrEqualCustomMessageModel { Value = 15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Value must be at most ten", results[0].ErrorMessage);
        }

        [TestMethod]
        public void LessThanOrEqual_DefaultMessage_ContainsLessThanOrEqual()
        {
            // Arrange
            var model = new LessThanOrEqualDefaultMessageModel { Value = 15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            StringAssert.Contains(results[0].ErrorMessage, "less than or equal");
        }
    }
}
