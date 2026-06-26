#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Misc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Misc
{
    [TestClass]
    public class ValidationCollectionNotEmptyTests
    {
        private static (bool isValid, List<ValidationResult> results) ValidateList(CollectionNotEmptyListModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        private static (bool isValid, List<ValidationResult> results) ValidateArray(CollectionNotEmptyArrayModel model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void CollectionNotEmpty_ListWithOneItem_IsValid()
        {
            // Arrange
            var model = new CollectionNotEmptyListModel { Items = new List<int> { 1 } };

            // Act
            var (isValid, _) = ValidateList(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CollectionNotEmpty_EmptyList_IsInvalid()
        {
            // Arrange
            var model = new CollectionNotEmptyListModel { Items = new List<int>() };

            // Act
            var (isValid, _) = ValidateList(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CollectionNotEmpty_NullList_IsInvalid()
        {
            // Arrange
            var model = new CollectionNotEmptyListModel { Items = null };

            // Act
            var (isValid, _) = ValidateList(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CollectionNotEmpty_ArrayWithTwoItems_IsValid()
        {
            // Arrange
            var model = new CollectionNotEmptyArrayModel { Items = new[] { 1, 2 } };

            // Act
            var (isValid, _) = ValidateArray(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CollectionNotEmpty_EmptyArray_IsInvalid()
        {
            // Arrange
            var model = new CollectionNotEmptyArrayModel { Items = new int[] { } };

            // Act
            var (isValid, _) = ValidateArray(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CollectionNotEmpty_NullArray_IsInvalid()
        {
            // Arrange
            var model = new CollectionNotEmptyArrayModel { Items = null };

            // Act
            var (isValid, _) = ValidateArray(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
