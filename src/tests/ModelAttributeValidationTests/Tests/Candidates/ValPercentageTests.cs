#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Candidates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Candidates
{
    [TestClass]
    public class ValPercentageTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValPercentage_MidRangeValue_IsValid()
        {
            // Arrange
            var model = new PercentageModel { Value = 50m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPercentage_LowerBoundary_IsValid()
        {
            // Arrange
            var model = new PercentageModel { Value = 0m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPercentage_UpperBoundary_IsValid()
        {
            // Arrange
            var model = new PercentageModel { Value = 100m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPercentage_NegativeOne_IsInvalid()
        {
            // Arrange
            var model = new PercentageModel { Value = -1m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPercentage_OneHundredAndOne_IsInvalid()
        {
            // Arrange
            var model = new PercentageModel { Value = 101m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPercentage_NullValue_IsInvalid()
        {
            // Arrange
            var model = new PercentageModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPercentage_Fraction_HalfValue_IsValid()
        {
            // Arrange
            var model = new PercentageFractionModel { Value = 0.5m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPercentage_Fraction_ValueAboveOne_IsInvalid()
        {
            // Arrange
            var model = new PercentageFractionModel { Value = 1.5m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
