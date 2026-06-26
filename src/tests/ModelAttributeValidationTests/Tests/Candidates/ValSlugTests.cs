#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Candidates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Candidates
{
    [TestClass]
    public class ValSlugTests
    {

        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValSlug_LowercaseWithHyphenAndDigits_IsValid()
        {
            // Arrange
            var model = new SlugModel { Slug = "my-post-123" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValSlug_LowercaseWordsHyphenated_IsValid()
        {
            // Arrange
            var model = new SlugModel { Slug = "my-post" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValSlug_UppercaseLetters_IsInvalid()
        {
            // Arrange
            var model = new SlugModel { Slug = "My-Post" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValSlug_LeadingHyphen_IsInvalid()
        {
            // Arrange
            var model = new SlugModel { Slug = "-bad" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValSlug_DoubleHyphen_IsInvalid()
        {
            // Arrange
            var model = new SlugModel { Slug = "a--b" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValSlug_NullValue_IsInvalid()
        {
            // Arrange
            var model = new SlugModel { Slug = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValSlug_Invalid_DefaultErrorMessageContainsSlugKeyword()
        {
            // Arrange
            var model = new SlugModel { Slug = "My-Post" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "slug");
        }

        [TestMethod]
        public void ValSlug_Invalid_CustomUserMessageIsReturned()
        {
            // Arrange
            var model = new SlugCustomMessageModel { Slug = "BAD" };

            // Act
            var (isValid, results) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("Custom slug error", results[0].ErrorMessage);
        }
    }
}
