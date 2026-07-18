using AttributeValidator.Mvvm.Tests.ViewModels;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeValidator.Mvvm.Tests.Tests
{
    [TestClass]
    public class ValidatableObservableObjectTests
    {
        [TestMethod]
        public void FreshInstance_HasErrors_IsFalse()
        {
            // Arrange / Act
            var vm = new PersonViewModel();

            // Assert
            Assert.IsFalse(vm.HasErrors);
        }

        [TestMethod]
        public void FreshInstance_GetErrors_Name_IsEmpty()
        {
            // Arrange
            var vm = new PersonViewModel();

            // Act
            var errors = vm.GetErrors("Name");

            // Assert
            var list = ToList(errors);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void SetProperty_RaisesPropertyChanged_WithCorrectPropertyName()
        {
            // Arrange
            var vm = new PersonViewModel();
            var raised = new List<string?>();
            vm.PropertyChanged += (_, e) => raised.Add(e.PropertyName);

            // Act
            vm.Name = "Alice";

            // Assert
            Assert.AreEqual(1, raised.Count);
            Assert.AreEqual("Name", raised[0]);
        }

        [TestMethod]
        public void SetInvalidEmail_HasErrors_IsTrue()
        {
            // Arrange
            var vm = new PersonViewModel();

            // Act
            vm.Email = "not-an-email";

            // Assert
            Assert.IsTrue(vm.HasErrors);
        }

        [TestMethod]
        public void SetInvalidEmail_GetErrors_Email_ContainsMessage()
        {
            // Arrange
            var vm = new PersonViewModel();

            // Act
            vm.Email = "not-an-email";

            // Assert
            var errors = ToList(vm.GetErrors("Email"));
            Assert.IsTrue(errors.Count > 0);
            Assert.IsFalse(string.IsNullOrEmpty(errors[0]));
        }

        [TestMethod]
        public void SetInvalidEmail_ErrorsChanged_FiredForEmail()
        {
            // Arrange
            var vm = new PersonViewModel();
            var firedProperties = new List<string?>();
            vm.ErrorsChanged += (_, e) => firedProperties.Add(e.PropertyName);

            // Act
            vm.Email = "not-an-email";

            // Assert
            CollectionAssert.Contains(firedProperties, "Email");
        }

        [TestMethod]
        public void SetValidEmail_AfterInvalid_ClearsErrors()
        {
            // Arrange
            var vm = new PersonViewModel();
            vm.Email = "not-an-email";

            // Act
            vm.Email = "a@b.com";

            // Assert
            var errors = ToList(vm.GetErrors("Email"));
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void SetValidEmail_AfterInvalid_ErrorsChanged_FiredForEmail()
        {
            // Arrange
            var vm = new PersonViewModel();
            vm.Email = "not-an-email";

            var firedProperties = new List<string?>();
            vm.ErrorsChanged += (_, e) => firedProperties.Add(e.PropertyName);

            // Act
            vm.Email = "a@b.com";

            // Assert
            CollectionAssert.Contains(firedProperties, "Email");
        }

        [TestMethod]
        public void SetEmptyName_HasError()
        {
            // Arrange
            var vm = new PersonViewModel();
            vm.Name = "Alice";

            // Act
            vm.Name = "";

            // Assert
            var errors = ToList(vm.GetErrors("Name"));
            Assert.IsTrue(errors.Count > 0);
        }

        [TestMethod]
        public void SetValidName_AfterEmpty_ClearsError()
        {
            // Arrange
            var vm = new PersonViewModel();
            vm.Name = "Alice";
            vm.Name = ""; // make invalid

            // Act
            vm.Name = "Alice";

            // Assert
            var errors = ToList(vm.GetErrors("Name"));
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void SetAgeToZero_AfterPositive_HasError()
        {
            // Arrange
            var vm = new PersonViewModel();
            vm.Age = 5; 

            // Act
            vm.Age = 0;

            // Assert
            var errors = ToList(vm.GetErrors("Age"));
            Assert.IsTrue(errors.Count > 0);
        }

        [TestMethod]
        public void SetAgeToPositive_AfterZero_ClearsError()
        {
            // Arrange
            var vm = new PersonViewModel();
            vm.Age = 5;
            vm.Age = 0; // make invalid

            // Act
            vm.Age = 5;

            // Assert
            var errors = ToList(vm.GetErrors("Age"));
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void SetProperty_SameValue_DoesNotRaisePropertyChangedAgain()
        {
            // Arrange
            var vm = new PersonViewModel();
            var count = 0;
            vm.PropertyChanged += (_, _) => count++;

            // Act
            vm.Name = "x";
            vm.Name = "x"; 

            // Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void SetProperty_SameValue_DoesNotRaiseErrorsChangedAgain()
        {
            // Arrange
            var vm = new PersonViewModel();
            var count = 0;
            vm.ErrorsChanged += (_, _) => count++;

            // Act
            vm.Email = "bad";
            var countAfterFirst = count;
            vm.Email = "bad";

            // Assert
            Assert.AreEqual(countAfterFirst, count);
        }

        [TestMethod]
        public void GetErrors_Null_ReturnsAllErrors_WhenMultiplePropertiesInvalid()
        {
            // Arrange
            var vm = new PersonViewModel();

            vm.Name = "Alice";
            vm.Name = "";

            // Make Email invalid
            vm.Email = "bad-email";

            // Act
            var allErrors = ToList(vm.GetErrors(null));

            // Assert
            Assert.IsTrue(allErrors.Count >= 2);
        }

        [TestMethod]
        public void GetErrors_EmptyString_ReturnsAllErrors_WhenMultiplePropertiesInvalid()
        {
            // Arrange
            var vm = new PersonViewModel();

            vm.Name = "Alice";
            vm.Name = "";

            vm.Email = "bad-email";

            // Act
            var allErrors = ToList(vm.GetErrors(string.Empty));

            // Assert
            Assert.IsTrue(allErrors.Count >= 2);
        }

        [TestMethod]
        public void ValidateAll_WithAllInvalidProperties_ReturnsFalse()
        {
            var vm = new PersonViewModel();

            // Act
            var result = vm.ValidateAll();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateAll_WithAllInvalidProperties_PopulatesHasErrors()
        {
            // Arrange
            var vm = new PersonViewModel();

            // Act
            vm.ValidateAll();

            // Assert
            Assert.IsTrue(vm.HasErrors);
        }

        [TestMethod]
        public void ValidateAll_WithAllInvalidProperties_GetErrors_NonEmpty()
        {
            // Arrange
            var vm = new PersonViewModel();

            // Act
            vm.ValidateAll();

            // Assert
            var allErrors = ToList(vm.GetErrors(null));
            Assert.IsTrue(allErrors.Count >= 1);
        }

        [TestMethod]
        public void ValidateAll_WithAllValidProperties_ReturnsTrue()
        {
            // Arrange
            var vm = new PersonViewModel
            {
                Name = "Alice",
                Email = "alice@example.com",
                Age = 30
            };

            // Act
            var result = vm.ValidateAll();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateAll_WithAllValidProperties_HasErrors_IsFalse()
        {
            // Arrange
            var vm = new PersonViewModel
            {
                Name = "Alice",
                Email = "alice@example.com",
                Age = 30
            };

            // Act
            vm.ValidateAll();

            // Assert
            Assert.IsFalse(vm.HasErrors);
        }

        [TestMethod]
        public void ValidateAll_ClearsStaleErrors_WhenFixedAfterPreviousValidateAll()
        {
            var vm = new PersonViewModel();
            vm.ValidateAll();

            vm.Name = "Bob";
            vm.Email = "bob@example.com";
            vm.Age = 25;

            // Act
            var result = vm.ValidateAll();

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(vm.HasErrors);
        }
        
        private static List<string> ToList(IEnumerable enumerable)
        {
            var result = new List<string>();
            foreach (var item in enumerable)
            {
                if (item is string s)
                    result.Add(s);
            }

            return result;
        }
    }
}
