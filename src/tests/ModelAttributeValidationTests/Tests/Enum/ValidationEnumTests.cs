#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Enum
{
    [TestClass]
    public class ValidationEnumTests
    {
        [TestMethod]
        public void EnumValidation_DefinedMember_IsValid()
        {
            // Arrange
            var model = new EnumModel { Status = OrderStatus.Shipped };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void EnumValidation_FirstDefinedMember_IsValid()
        {
            // Arrange
            var model = new EnumModel { Status = OrderStatus.Pending };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void EnumValidation_AllDefinedMembers_AreValid()
        {
            // Arrange
            var defined = new[] { OrderStatus.Pending, OrderStatus.Processing, OrderStatus.Shipped, OrderStatus.Delivered };
            foreach (var status in defined)
            {
                var model = new EnumModel { Status = status };
                var context = new ValidationContext(model, null, null);
                var validationResults = new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(model, context, validationResults, true);

                Assert.IsTrue(isValid, $"Expected OrderStatus.{status} to be valid but it was not.");
            }
        }

        [TestMethod]
        public void EnumValidation_OutOfRangeCastValue_IsInvalid()
        {
            // Arrange
            var model = new EnumModel { Status = (OrderStatus)999 };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void EnumValidation_OutOfRangeCastValue_ErrorMessageContainsPropertyName()
        {
            // Arrange
            var model = new EnumModel { Status = (OrderStatus)999 };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(validationResults.Count > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(validationResults[0].ErrorMessage));
            StringAssert.Contains(validationResults[0].ErrorMessage, "Status");
        }
    }
}
