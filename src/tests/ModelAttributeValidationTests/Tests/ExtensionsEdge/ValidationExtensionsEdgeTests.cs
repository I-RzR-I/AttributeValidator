#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.ExtensionsEdge;
using RzR.Validation.Attributes.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.ExtensionsEdge
{
    [TestClass]
    public class ValidationExtensionsEdgeTests
    {
        [TestMethod]
        public void ValidateAndThrow_ValidInstance_DoesNotThrow()
        {
            // Arrange
            var model = new ValidSimpleModel
            {
                Name = "Alice"
            };

            // Act & Assert
            model.ValidateAndThrow();
        }

        [TestMethod]
        public void ValidateAndThrow_ValidInstance_WithExplicitNullServiceProvider_DoesNotThrow()
        {
            // Arrange
            var model = new ValidSimpleModel
            {
                Name = "Alice"
            };

            // Act & Assert
            model.ValidateAndThrow(serviceProvider: null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateAndThrow_InvalidInstance_ThrowsValidationException()
        {
            // Arrange
            var model = new InvalidSimpleModel
            {
                Name = null
            };

            // Act — must throw
            model.ValidateAndThrow();
        }

        [TestMethod]
        public void ValidateAndThrow_InvalidInstance_ThrowsValidationException_AssertVariant()
        {
            // Arrange
            var model = new InvalidSimpleModel
            {
                Name = null
            };

            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => model.ValidateAndThrow());
        }

        [TestMethod]
        public void IsValid_NullInstance_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => ValidationExtensions.IsValid(null));
        }

        [TestMethod]
        public void Validate_NullInstance_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => ValidationExtensions.Validate(null));
        }

        [TestMethod]
        public void TryValidate_NullInstance_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                ValidationExtensions.TryValidate(null, out _);
            });
        }

        [TestMethod]
        public void ValidateAndThrow_NullInstance_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => ValidationExtensions.ValidateAndThrow(null));
        }

        [TestMethod]
        public void IsValid_NullInstance_WithServiceProvider_ThrowsArgumentNullException()
        {
            var dummyProvider = new DummyServiceProvider();

            Assert.ThrowsException<ArgumentNullException>(
                () => ValidationExtensions.IsValid(null, dummyProvider));
        }

        [TestMethod]
        public void ValidateAndThrow_NullInstance_WithServiceProvider_ThrowsArgumentNullException()
        {
            var dummyProvider = new DummyServiceProvider();

            Assert.ThrowsException<ArgumentNullException>(
                () => ValidationExtensions.ValidateAndThrow(null, dummyProvider));
        }

        [TestMethod]
        public void IsValid_ClassLevelAtLeastOneOf_BothFieldsNull_ReturnsFalse()
        {
            // Arrange
            var model = new AtLeastOneContactEdgeModel
            {
                Email = null,
                Phone = null
            };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_ClassLevelAtLeastOneOf_OneFieldSet_ReturnsTrue()
        {
            // Arrange
            var model = new AtLeastOneContactEdgeModel
            {
                Email = "qa@example.com",
                Phone = null
            };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_ClassLevelAtLeastOneOf_BothFieldsNull_ReturnsNonEmptyResults()
        {
            // Arrange
            var model = new AtLeastOneContactEdgeModel
            {
                Email = null,
                Phone = null
            };

            // Act
            var results = model.Validate();

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void TryValidate_ClassLevelAtLeastOneOf_BothFieldsNull_ReturnsFalseAndPopulatesResults()
        {
            // Arrange
            var model = new AtLeastOneContactEdgeModel
            {
                Email = null,
                Phone = null
            };

            // Act
            var isValid = model.TryValidate(out var results);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void ValidateAndThrow_ClassLevelAtLeastOneOf_BothFieldsNull_ThrowsValidationException()
        {
            // Arrange
            var model = new AtLeastOneContactEdgeModel
            {
                Email = null,
                Phone = null
            };

            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => model.ValidateAndThrow());
        }

        [TestMethod]
        public void Validate_ClassLevelAtLeastOneOf_Valid_ReturnsEmptyResults()
        {
            // Arrange
            var model = new AtLeastOneContactEdgeModel
            {
                Email = null,
                Phone = "+40700000000"
            };

            // Act
            var results = model.Validate();

            // Assert
            Assert.AreEqual(0, results.Count);
        }
        [TestMethod]
        public void IsValid_CrossProperty_RequiredIf_ConditionMet_FieldNull_ReturnsFalse()
        {
            // Arrange
            var model = new RequiredIfEdgeModel
            {
                PaymentType = "Card", 
                CardNumber = null
            };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_CrossProperty_RequiredIf_ConditionMet_FieldPopulated_ReturnsTrue()
        {
            // Arrange
            var model = new RequiredIfEdgeModel
            {
                PaymentType = "Card",
                CardNumber = "4111111111111111"
            };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_CrossProperty_RequiredIf_ConditionNotMet_FieldNull_ReturnsTrue()
        {
            // Arrange
            var model = new RequiredIfEdgeModel
            {
                PaymentType = "Cash",
                CardNumber = null
            };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_CrossProperty_RequiredIf_ConditionMet_FieldNull_ReturnsNonEmptyResults()
        {
            // Arrange
            var model = new RequiredIfEdgeModel
            {
                PaymentType = "Card",
                CardNumber = null
            };

            // Act
            var results = model.Validate();

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void TryValidate_CrossProperty_RequiredIf_ConditionMet_FieldNull_ReturnsFalseAndPopulatesResults()
        {
            // Arrange
            var model = new RequiredIfEdgeModel
            {
                PaymentType = "Card",
                CardNumber = null
            };

            // Act
            var isValid = model.TryValidate(out var results);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void ValidateAndThrow_CrossProperty_RequiredIf_ConditionMet_FieldNull_ThrowsValidationException()
        {
            // Arrange
            var model = new RequiredIfEdgeModel
            {
                PaymentType = "Card",
                CardNumber = null
            };

            // Act & Assert
            Assert.ThrowsException<ValidationException>(
                () => model.ValidateAndThrow(),
                "ValidateAndThrow() must throw when a [ValRequiredIf] condition is violated.");
        }

        private sealed class DummyServiceProvider : IServiceProvider
        {
            public object GetService(Type serviceType) => null;
        }
    }
}
