#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Candidates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Candidates
{
    [TestClass]
    public class ValLatitudeTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValLatitude_TypicalMidRangeValue_IsValid()
        {
            // Arrange
            var model = new LatitudeModel { Latitude = 45.0m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValLatitude_LowerBoundaryNegative90_IsValid()
        {
            // Arrange
            var model = new LatitudeModel { Latitude = -90m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValLatitude_UpperBoundary90_IsValid()
        {
            // Arrange
            var model = new LatitudeModel { Latitude = 90m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValLatitude_AboveUpperBound_IsInvalid()
        {
            // Arrange
            var model = new LatitudeModel { Latitude = 91m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValLatitude_BelowLowerBound_IsInvalid()
        {
            // Arrange
            var model = new LatitudeModel { Latitude = -91m };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValLatitude_NullValue_IsInvalid()
        {
            // Arrange
            var model = new LatitudeModel { Latitude = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
