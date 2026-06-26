#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CandidatesStr;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CandidatesStr
{
    [TestClass]
    public class ValColorNameTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValColorName_LowercaseRed_IsValid()
        {
            // Arrange
            var model = new ColorNameModel { Value = "red" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValColorName_MixedCaseRebeccaPurple_IsValid()
        {
            // Arrange
            var model = new ColorNameModel { Value = "RebeccaPurple" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValColorName_CornflowerBlue_IsValid()
        {
            // Arrange
            var model = new ColorNameModel { Value = "CornflowerBlue" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValColorName_UnknownColorName_IsInvalid()
        {
            // Arrange
            var model = new ColorNameModel { Value = "notacolor" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValColorName_EmptyString_IsInvalid()
        {
            // Arrange
            var model = new ColorNameModel { Value = "" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValColorName_NullValue_IsInvalid()
        {
            // Arrange
            var model = new ColorNameModel { Value = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValColorName_InvalidValue_DefaultErrorMessageContainsColor()
        {
            // Arrange
            var model = new ColorNameModel { Value = "notacolor" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "color");
        }

        [TestMethod]
        public void ValColorName_InvalidValue_CustomUserMessageIsReturned()
        {
            // Arrange
            var model = new ColorNameCustomMessageModel { Value = "notacolor" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("Please provide a valid CSS color name.", results[0].ErrorMessage);
        }
    }
}
