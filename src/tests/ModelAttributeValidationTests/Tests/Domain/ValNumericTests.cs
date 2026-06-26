#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Domain
{
    [TestClass]
    public class ValNumericTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void DecimalPrecision_ValidValue_123_45_IsValid()
        {
            // Arrange
            var model = new DecimalPrecisionModel { Amount = 123.45m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void DecimalPrecision_TooManyDecimalPlaces_123_456_IsInvalid()
        {
            // Arrange
            var model = new DecimalPrecisionModel { Amount = 123.456m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void DecimalPrecision_TotalDigitsExceedPrecision_123456_IsInvalid()
        {
            // Arrange
            var model = new DecimalPrecisionModel { Amount = 123456m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void DecimalPrecision_Null_IsInvalid()
        {
            // Arrange
            var model = new DecimalPrecisionModel { Amount = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void DecimalPrecision_SingleDigit_IsValid()
        {
            // Arrange
            var model = new DecimalPrecisionModel { Amount = 9m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void DecimalPrecision_BoundaryValue_99999_IsValid()
        {
            // Arrange
            var model = new DecimalPrecisionModel { Amount = 99999m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void DecimalPrecision_BoundaryDecimalScale_12_34_IsValid()
        {
            // Arrange
            var model = new DecimalPrecisionModel { Amount = 12.34m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MultipleOfInt_MultipleOf5_15_IsValid()
        {
            // Arrange
            var model = new MultipleOfIntModel { Quantity = 15 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MultipleOfInt_NotMultipleOf5_13_IsInvalid()
        {
            // Arrange
            var model = new MultipleOfIntModel { Quantity = 13 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MultipleOfInt_Zero_IsValid()
        {
            // Arrange
            var model = new MultipleOfIntModel { Quantity = 0 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MultipleOfInt_NegativeMultiple_IsValid()
        {
            // Arrange
            var model = new MultipleOfIntModel { Quantity = -10 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MultipleOfDecimal_1Point5_IsValid()
        {
            // Arrange
            var model = new MultipleOfDecimalModel { Price = 1.5m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MultipleOfDecimal_1Point7_IsInvalid()
        {
            // Arrange
            var model = new MultipleOfDecimalModel { Price = 1.7m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MultipleOf_NullValue_IsInvalid()
        {
            // Arrange
            var model = new MultipleOfNullableModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
