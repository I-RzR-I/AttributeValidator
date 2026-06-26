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
    public class ValidationLessThanTests
    {
        [TestMethod]
        public void LessThan_Int_ValueLessThanThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanIntModel { Value = 3 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThan_Int_ValueGreaterThanThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanIntModel { Value = 15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThan_Int_ValueEqualToThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanIntModel { Value = 10 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThan_Decimal_FractionalValueBelowThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanDecimalModel { Value = 5.4m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThan_Decimal_FractionalValueAboveThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanDecimalModel { Value = 5.6m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThan_Double_FractionalValueBelowThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanDoubleModel { Value = 3.13 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThan_Double_FractionalValueAboveThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanDoubleModel { Value = 3.15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThan_DateTime_ValueBeforeThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanDateTimeModel { Value = new DateTime(2024, 6, 1) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThan_DateTime_ValueAfterThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanDateTimeModel { Value = new DateTime(2026, 1, 1) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThan_NullValue_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new LessThanNullableIntModel { Value = null };
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
        public void LessThan_UnsupportedType_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new LessThanUnsupportedTypeModel { Value = false };
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
        public void LessThan_CustomMessage_FailureReturnsCustomMessage()
        {
            // Arrange
            var model = new LessThanCustomMessageModel { Value = 15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Value must be less than ten", results[0].ErrorMessage);
        }

        [TestMethod]
        public void LessThan_DefaultMessage_ContainsMustBeLessThan()
        {
            // Arrange
            var model = new LessThanDefaultMessageModel { Value = 15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            StringAssert.Contains(results[0].ErrorMessage, "must be less than");
        }
    }
}
