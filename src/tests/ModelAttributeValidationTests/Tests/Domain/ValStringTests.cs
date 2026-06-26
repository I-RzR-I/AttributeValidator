#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Domain
{
    [TestClass]
    public class ValStringTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void LengthRange_WithinRange_IsValid()
        {
            // Arrange
            var model = new LengthRangeModel { Code = "abc" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LengthRange_TooShort_IsInvalid()
        {
            // Arrange
            var model = new LengthRangeModel { Code = "a" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LengthRange_TooLong_IsInvalid()
        {
            // Arrange
            var model = new LengthRangeModel { Code = "abcdef" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LengthRange_Null_IsInvalid()
        {
            // Arrange
            var model = new LengthRangeModel { Code = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LengthRange_AtMinBoundary_IsValid()
        {
            // Arrange
            var model = new LengthRangeModel { Code = "ab" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LengthRange_AtMaxBoundary_IsValid()
        {
            // Arrange
            var model = new LengthRangeModel { Code = "abcde" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void StartsWith_CorrectPrefix_IsValid()
        {
            // Arrange
            var model = new StartsWithModel { InvoiceNumber = "INV-001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void StartsWith_WrongPrefix_IsInvalid()
        {
            // Arrange
            var model = new StartsWithModel { InvoiceNumber = "001-INV" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void StartsWith_Null_IsInvalid()
        {
            // Arrange
            var model = new StartsWithModel { InvoiceNumber = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void StartsWith_CaseSensitive_WrongCase_IsInvalid()
        {
            // Arrange
            var model = new StartsWithModel { InvoiceNumber = "inv-001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void StartsWithIgnoreCase_LowerCaseInput_IsValid()
        {
            // Arrange
            var model = new StartsWithIgnoreCaseModel { InvoiceNumber = "inv-001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void StartsWithIgnoreCase_WrongPrefix_IsInvalid()
        {
            // Arrange
            var model = new StartsWithIgnoreCaseModel { InvoiceNumber = "ORD-001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void EndsWith_ValidSuffix_IsValid()
        {
            // Arrange
            var model = new EndsWithModel { FileName = "document.pdf" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void EndsWith_WrongSuffix_IsInvalid()
        {
            // Arrange
            var model = new EndsWithModel { FileName = "document.txt" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void EndsWith_Null_IsInvalid()
        {
            // Arrange
            var model = new EndsWithModel { FileName = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Contains_HasSubstring_IsValid()
        {
            // Arrange
            var model = new ContainsModel { Email = "a@b.com" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Contains_MissingSubstring_IsInvalid()
        {
            // Arrange
            var model = new ContainsModel { Email = "noatsign.com" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Contains_Null_IsInvalid()
        {
            // Arrange
            var model = new ContainsModel { Email = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Regex_MatchingPattern_IsValid()
        {
            // Arrange
            var model = new RegexModel { Code = "123" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Regex_NonMatchingPattern_IsInvalid()
        {
            // Arrange
            var model = new RegexModel { Code = "12a" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Regex_Null_IsInvalid()
        {
            // Arrange
            var model = new RegexModel { Code = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Regex_TooManyDigits_IsInvalid()
        {
            // Arrange
            var model = new RegexModel { Code = "1234" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Regex_InvalidPattern_DoesNotThrow_ReturnsInvalid()
        {
            // Arrange
            var model = new RegexBadPatternModel { Value = "anything" };

            // Act — no exception must escape.
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
