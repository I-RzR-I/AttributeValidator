#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Positive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Positive
{
    [TestClass]
    public class ValidationPositiveRegressionTests
    {
        [TestMethod]
        public void Positive_Uint_ValueOne_IsValid()
        {
            // Arrange
            var model = new PositiveUnsignedModel { UIntNumber = 1u, ByteNumber = 1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Positive_Uint_ValueZero_IsInvalid()
        {
            // Arrange
            var model = new PositiveUnsignedModel { UIntNumber = 0u, ByteNumber = 1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_Byte_ValueOne_IsValid()
        {
            // Arrange
            var model = new PositiveUnsignedModel { UIntNumber = 1u, ByteNumber = 1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Positive_Byte_ValueZero_IsInvalid()
        {
            // Arrange
            var model = new PositiveUnsignedModel { UIntNumber = 1u, ByteNumber = 0 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_Sbyte_NegativeValue_IsInvalid()
        {
            // Arrange
            var model = new PositiveSbyteModel { SbyteNumber = -1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_Short_NegativeValue_IsInvalid()
        {
            // Arrange
            var model = new PositiveShortNegativeModel { ShortNumber = -1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_Int_NegativeValue_IsInvalid()
        {
            // Arrange
            var model = new PositiveIntNegativeModel { IntNumber = -1 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_Long_NegativeValue_IsInvalid()
        {
            // Arrange
            var model = new PositiveLongNegativeModel { LongNumber = -1L };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_Decimal_NegativeValue_IsInvalid()
        {
            // Arrange
            var model = new PositiveDecimalNegativeModel { DecimalNumber = -1m };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_Double_NegativeValue_IsInvalid()
        {
            // Arrange
            var model = new PositiveDoubleNegativeModel { DoubleNumber = -1d };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_NullValue_IsInvalid_AndDoesNotThrow()
        {
            // Arrange
            var model = new PositiveNullableModel();
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            Exception caughtException = null;
            var isValid = false;
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
        public void Positive_DefaultMessage_ContainsMustBeGreaterThanZero_NotMustNotBe()
        {
            // Arrange
            var model = new PositiveDefaultMessageModel { IntNumber = 0 };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            StringAssert.Contains(results[0].ErrorMessage, "must be greater than 0");
            Assert.IsFalse(results[0].ErrorMessage.Contains("must not be"));
        }
    }
}
