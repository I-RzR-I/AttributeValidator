#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Greater;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Greater
{
    [TestClass]
    public class ValidationGreaterThanOrEqualTests
    {
        [TestMethod]
        public void GreaterThanOrEqual_Int_ValueGreaterThanThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanOrEqualIntModel { Value = 10 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_Int_ValueLessThanThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanOrEqualIntModel { Value = 3 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_Int_ValueEqualToThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanOrEqualIntModel { Value = 5 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_Decimal_FractionalValueAboveThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanOrEqualDecimalModel { Value = 5.6m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_Decimal_FractionalValueBelowThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanOrEqualDecimalModel { Value = 5.4m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_Double_FractionalValueAboveThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanOrEqualDoubleModel { Value = 3.15 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_Double_FractionalValueBelowThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanOrEqualDoubleModel { Value = 3.13 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_DateTime_ValueAfterThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanOrEqualDateTimeModel { Value = new DateTime(2025, 6, 1) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_DateTime_ValueBeforeThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanOrEqualDateTimeModel { Value = new DateTime(2023, 6, 1) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_NullValue_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new GreaterThanOrEqualNullableIntModel { Value = null };
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
            Assert.IsNull(caughtException, "Null value must NOT cause an exception.");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThanOrEqual_UnsupportedType_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new GreaterThanOrEqualUnsupportedTypeModel { Value = true };
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
        public void GreaterThanOrEqual_CustomMessage_FailureReturnsCustomMessage()
        {
            // Arrange
            var model = new GreaterThanOrEqualCustomMessageModel { Value = 3 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Value must be at least ten", results[0].ErrorMessage);
        }

        [TestMethod]
        public void GreaterThanOrEqual_DefaultMessage_ContainsGreaterThanOrEqual()
        {
            // Arrange
            var model = new GreaterThanOrEqualDefaultMessageModel { Value = 3 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            StringAssert.Contains(results[0].ErrorMessage, "greater than or equal");
        }
    }
}
