#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Identity
{
    [TestClass]
    public class ValCountryCodeTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValCountryCode_US_Uppercase_IsValid()
        {
            // Arrange
            var model = new CountryCodeModel { Code = "US" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCountryCode_US_Lowercase_IsValid()
        {
            // Arrange
            var model = new CountryCodeModel { Code = "us" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCountryCode_RO_IsValid()
        {
            // Arrange
            var model = new CountryCodeModel { Code = "RO" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCountryCode_ZZ_FictitiousCode_IsInvalid()
        {
            // Arrange
            var model = new CountryCodeModel { Code = "ZZ" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCountryCode_USA_ThreeLetters_IsInvalid()
        {
            // Arrange
            var model = new CountryCodeModel { Code = "USA" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCountryCode_NullValue_IsInvalid()
        {
            // Arrange
            var model = new CountryCodeModel { Code = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
