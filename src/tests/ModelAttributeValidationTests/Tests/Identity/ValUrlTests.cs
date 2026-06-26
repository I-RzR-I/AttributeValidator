#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Identity
{
    [TestClass]
    public class ValUrlTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValUrl_HttpsWithPathAndQuery_IsValid()
        {
            // Arrange
            var model = new UrlModel { Url = "https://x.com/p?q=1" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValUrl_HttpUrl_IsValid()
        {
            // Arrange
            var model = new UrlModel { Url = "http://example.com" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValUrl_JavascriptScheme_IsInvalid()
        {
            // Arrange
            var model = new UrlModel { Url = "javascript:alert(1)" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUrl_NotAUrl_IsInvalid()
        {
            // Arrange
            var model = new UrlModel { Url = "notaurl" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUrl_NullValue_IsInvalid()
        {
            // Arrange
            var model = new UrlModel { Url = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUrl_RequireHttps_HttpUrl_IsInvalid()
        {
            // Arrange
            var model = new UrlRequireHttpsModel { Url = "http://x.com" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValUrl_RequireHttps_HttpsUrl_IsValid()
        {
            // Arrange
            var model = new UrlRequireHttpsModel { Url = "https://x.com" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
