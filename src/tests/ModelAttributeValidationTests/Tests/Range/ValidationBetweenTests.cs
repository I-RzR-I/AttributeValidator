#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Range;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Range
{
    [TestClass]
    public class ValidationBetweenTests
    {
        [TestMethod]
        public void Between_Inclusive_ValueEqualsMin_IsValid()
        {
            // Arrange
            var model = new BetweenInclusiveIntModel { Value = 1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Between_Inclusive_ValueEqualsMax_IsValid()
        {
            // Arrange
            var model = new BetweenInclusiveIntModel { Value = 10 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Between_Inclusive_ValueInsideRange_IsValid()
        {
            // Arrange
            var model = new BetweenInclusiveIntModel { Value = 5 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Between_Inclusive_ValueBelowMin_IsInvalid()
        {
            // Arrange
            var model = new BetweenInclusiveIntModel { Value = 0 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Between_Inclusive_ValueAboveMax_IsInvalid()
        {
            // Arrange
            var model = new BetweenInclusiveIntModel { Value = 11 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Between_Exclusive_ValueEqualsMin_IsInvalid()
        {
            // Arrange
            var model = new BetweenExclusiveIntModel { Value = 1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Between_Exclusive_ValueEqualsMax_IsInvalid()
        {
            // Arrange
            var model = new BetweenExclusiveIntModel { Value = 10 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Between_Exclusive_ValueStrictlyInsideRange_IsValid()
        {
            // Arrange
            var model = new BetweenExclusiveIntModel { Value = 5 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Between_Exclusive_ValueOutsideRange_IsInvalid()
        {
            // Arrange
            var model = new BetweenExclusiveIntModel { Value = 0 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Between_Decimal_FractionalValueInsideRange_IsValid()
        {
            // Arrange
            var model = new BetweenDecimalModel { Value = 2.5m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Between_Decimal_FractionalValueOutsideRange_IsInvalid()
        {
            // Arrange
            var model = new BetweenDecimalModel { Value = 5.1m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Between_DateTime_ValueInsideRange_IsValid()
        {
            // Arrange
            var model = new BetweenDateTimeModel { Value = new DateTime(2024, 6, 15) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Between_DateTime_ValueOutsideRange_IsInvalid()
        {
            // Arrange
            var model = new BetweenDateTimeModel { Value = new DateTime(2026, 1, 1) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Between_NullValue_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new BetweenNullableIntModel { Value = null };
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
        public void Between_UnsupportedType_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new BetweenUnsupportedTypeModel { Value = true };
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
        public void Between_CustomMessage_FailureReturnsCustomMessage()
        {
            // Arrange
            var model = new BetweenCustomMessageModel { Value = 0 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Value must be between 1 and 10", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Between_DefaultMessage_ContainsBetween()
        {
            // Arrange
            var model = new BetweenDefaultMessageModel { Value = 0 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            StringAssert.Contains(results[0].ErrorMessage, "between");
        }
    }
}
