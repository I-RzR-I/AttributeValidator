#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Candidates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Candidates
{
    [TestClass]
    public class ValLongitudeTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValLongitude_TypicalMidRangeValue_IsValid()
        {
            // Arrange
            var model = new LongitudeModel { Longitude = 120.5m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValLongitude_LowerBoundaryNegative180_IsValid()
        {
            // Arrange
            var model = new LongitudeModel { Longitude = -180m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValLongitude_UpperBoundary180_IsValid()
        {
            // Arrange
            var model = new LongitudeModel { Longitude = 180m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValLongitude_AboveUpperBound_IsInvalid()
        {
            // Arrange
            var model = new LongitudeModel { Longitude = 181m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValLongitude_NullValue_IsInvalid()
        {
            // Arrange
            var model = new LongitudeModel { Longitude = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
