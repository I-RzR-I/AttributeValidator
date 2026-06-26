#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Identity
{
    [TestClass]
    public class ValUsernameTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValUsername_AlphanumericWithUnderscore_IsValid()
        {
            // Arrange
            var model = new UsernameModel { Username = "john_doe" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValUsername_TwoCharacters_BelowDefaultMin_IsInvalid()
        {
            // Arrange
            var model = new UsernameModel { Username = "ab" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUsername_StartsWithHyphen_IsInvalid()
        {
            // Arrange
            var model = new UsernameModel { Username = "-bad" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUsername_ContainsSpace_IsInvalid()
        {
            // Arrange
            var model = new UsernameModel { Username = "has space" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUsername_25Characters_AboveDefaultMax_IsInvalid()
        {
            // Arrange
            var model = new UsernameModel { Username = "aaaaaaaaaaaaaaaaaaaaaaaaa" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUsername_NullValue_IsInvalid()
        {
            // Arrange
            var model = new UsernameModel { Username = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUsername_CustomBoundsMin2_TwoCharacters_IsValid()
        {
            // Arrange
            var model = new UsernameCustomBoundsModel { Username = "ab" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
