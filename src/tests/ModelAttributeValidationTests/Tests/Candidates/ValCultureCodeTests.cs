#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Candidates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Candidates
{
    [TestClass]
    public class ValCultureCodeTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValCultureCode_TwoLetterCode_IsValid()
        {
            // Arrange
            var model = new CultureCodeModel { Culture = "en" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCultureCode_LanguageRegionCode_IsValid()
        {
            // Arrange
            var model = new CultureCodeModel { Culture = "en-US" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCultureCode_LanguageScriptRegionCode_IsValid()
        {
            // Arrange
            var model = new CultureCodeModel { Culture = "zh-Hans-CN" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValCultureCode_FullWordLanguageName_IsInvalid()
        {
            // Arrange
            var model = new CultureCodeModel { Culture = "english" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCultureCode_UnderscoreSeparator_IsInvalid()
        {
            // Arrange
            var model = new CultureCodeModel { Culture = "EN_us" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCultureCode_NullValue_IsInvalid()
        {
            // Arrange
            var model = new CultureCodeModel { Culture = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValCultureCode_Invalid_DefaultErrorMessageContainsCultureKeyword()
        {
            // Arrange
            var model = new CultureCodeModel { Culture = "english" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "culture");
        }

        [TestMethod]
        public void ValCultureCode_Invalid_CustomUserMessageIsReturned()
        {
            // Arrange
            var model = new CultureCodeCustomMessageModel { Culture = "english" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("Custom culture error", results[0].ErrorMessage);
        }
    }
}
