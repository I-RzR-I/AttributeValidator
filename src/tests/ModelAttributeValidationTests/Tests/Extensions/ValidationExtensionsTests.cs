#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Extensions;
using RzR.Validation.Attributes.Extensions;
using System.Linq;

#endregion

namespace ModelAttributeValidationTests.Tests.Extensions
{
    [TestClass]
    public class ValidationExtensionsTests
    {
        private static ValidationExtensionsModel ValidModel()
            => new ValidationExtensionsModel { Name = "Widget", Quantity = 5 };

        private static ValidationExtensionsModel BothInvalidModel()
            => new ValidationExtensionsModel { Name = null, Quantity = 0 };

        private static ValidationExtensionsModel NameInvalidModel()
            => new ValidationExtensionsModel { Name = "", Quantity = 1 };

        private static ValidationExtensionsModel QuantityInvalidModel()
            => new ValidationExtensionsModel { Name = "Widget", Quantity = 0 };

        [TestMethod]
        public void TryValidate_ValidInstance_ReturnsTrue()
        {
            // Arrange
            var model = ValidModel();

            // Act
            var result = model.TryValidate(out _);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TryValidate_ValidInstance_OutResultsIsEmpty()
        {
            // Arrange
            var model = ValidModel();

            // Act
            model.TryValidate(out var results);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TryValidate_InvalidName_ReturnsFalse()
        {
            // Arrange
            var model = NameInvalidModel();

            // Act
            var result = model.TryValidate(out _);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryValidate_InvalidName_OutResultsIsNonEmpty()
        {
            // Arrange
            var model = NameInvalidModel();

            // Act
            model.TryValidate(out var results);

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void TryValidate_InvalidName_ResultMemberNameIsName()
        {
            // Arrange
            var model = NameInvalidModel();

            // Act
            model.TryValidate(out var results);

            // Assert
            var memberNames = results
                .SelectMany(r => r.MemberNames)
                .ToList();
            CollectionAssert.Contains(memberNames, nameof(ValidationExtensionsModel.Name));
        }

        [TestMethod]
        public void TryValidate_InvalidQuantity_ReturnsFalse()
        {
            // Arrange
            var model = QuantityInvalidModel();

            // Act
            var result = model.TryValidate(out _);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryValidate_InvalidQuantity_ResultMemberNameIsQuantity()
        {
            // Arrange
            var model = QuantityInvalidModel();

            // Act
            model.TryValidate(out var results);

            // Assert
            var memberNames = results
                .SelectMany(r => r.MemberNames)
                .ToList();
            CollectionAssert.Contains(memberNames, nameof(ValidationExtensionsModel.Quantity));
        }

        [TestMethod]
        public void TryValidate_BothPropertiesInvalid_ReturnsFalse()
        {
            // Arrange
            var model = BothInvalidModel();

            // Act
            var result = model.TryValidate(out _);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryValidate_BothPropertiesInvalid_BothMemberNamesAppearInResults()
        {
            // Arrange
            var model = BothInvalidModel();

            // Act
            model.TryValidate(out var results);

            // Assert
            var memberNames = results
                .SelectMany(r => r.MemberNames)
                .ToList();

            CollectionAssert.Contains(memberNames, nameof(ValidationExtensionsModel.Name));
            CollectionAssert.Contains(memberNames, nameof(ValidationExtensionsModel.Quantity));
        }

        [TestMethod]
        public void TryValidate_BothPropertiesInvalid_ResultsCountIsAtLeastTwo()
        {
            // Arrange
            var model = BothInvalidModel();

            // Act
            model.TryValidate(out var results);

            // Assert
            Assert.IsTrue(results.Count >= 2);
        }

        [TestMethod]
        public void Validate_ValidInstance_ReturnsEmptyCollection()
        {
            // Arrange
            var model = ValidModel();

            // Act
            var results = model.Validate();

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Validate_InvalidName_ReturnsNonEmptyCollection()
        {
            // Arrange
            var model = NameInvalidModel();

            // Act
            var results = model.Validate();

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void Validate_InvalidQuantity_ReturnsNonEmptyCollection()
        {
            // Arrange
            var model = QuantityInvalidModel();

            // Act
            var results = model.Validate();

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void Validate_BothPropertiesInvalid_ReturnsBothMemberNames()
        {
            // Arrange
            var model = BothInvalidModel();

            // Act
            var results = model.Validate();

            // Assert
            var memberNames = results
                .SelectMany(r => r.MemberNames)
                .ToList();

            CollectionAssert.Contains(memberNames, nameof(ValidationExtensionsModel.Name));
            CollectionAssert.Contains(memberNames, nameof(ValidationExtensionsModel.Quantity));
        }

        [TestMethod]
        public void IsValid_ValidInstance_ReturnsTrue()
        {
            // Arrange
            var model = ValidModel();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_InvalidName_ReturnsFalse()
        {
            // Arrange
            var model = NameInvalidModel();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_InvalidQuantity_ReturnsFalse()
        {
            // Arrange
            var model = QuantityInvalidModel();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_BothPropertiesInvalid_ReturnsFalse()
        {
            // Arrange
            var model = BothInvalidModel();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
