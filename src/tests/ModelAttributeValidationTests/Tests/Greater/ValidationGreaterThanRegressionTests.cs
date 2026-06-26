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
    public class ValidationGreaterThanRegressionTests
    {
        [TestMethod]
        public void GreaterThan_DateTime_ValueGreaterThanThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanDateTimeModel
            {
                Date = new DateTime(2025, 1, 1)
            };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThan_DateTime_ValueLessThanThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanDateTimeModel
            {
                Date = new DateTime(2023, 6, 1)
            };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_TimeSpan_ValueGreaterThanThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanTimeSpanModel
            {
                Duration = TimeSpan.FromHours(2)
            };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThan_TimeSpan_ValueLessThanThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanTimeSpanModel
            {
                Duration = TimeSpan.FromMinutes(30)
            };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_DateTime_ValueEqualToThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanDateTimeEqualModel { Date = new DateTime(2024, 6, 15) };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_Int_ValueEqualToThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanModel
            {
                LongNumber = 5,
                ShortNumber = 11,
                UnSignedShortNumber = 2,
                DecimalNumber = 6m,
                FloatNumber = 7f,
                DoubleNumber = 57d
            };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_Uint_ValueGreaterThanThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanUnsignedModel { UIntNumber = 1u, ByteNumber = 1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThan_Uint_ValueEqualToThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanUnsignedModel { UIntNumber = 0u, ByteNumber = 0 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_Byte_ValueGreaterThanThreshold_IsValid()
        {
            // Arrange
            var model = new GreaterThanUnsignedModel { UIntNumber = 1u, ByteNumber = 5 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThan_NullValue_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new GreaterThanNullRobustnessModel { NullableInt = null };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act — must not throw
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
        public void GreaterThan_CustomMessage_FailureReturnsCustomMessage()
        {
            // Arrange
            var model = new GreaterThanCustomMessageModel
            {
                IntWithCustomMessage = 5
            };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Value must exceed ten", results[0].ErrorMessage);
        }

        [TestMethod]
        public void GreaterThan_DefaultMessage_ContainsMustBeGreaterThan_NotMustNotBe()
        {
            // Arrange
            var model = new GreaterThanDefaultMessageModel
            {
                IntWithDefaultMessage = 3
            };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            StringAssert.Contains(results[0].ErrorMessage, "must be greater than");
            Assert.IsFalse(results[0].ErrorMessage.Contains("must not be"));
        }
    }
}
