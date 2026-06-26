#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.NotDefault;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.NotDefault
{
    [TestClass]
    public class ValidationNotDefaultNullableTests
    {
        [TestMethod]
        public void NotDefault_Nullable_False_Test2()
        {
            var model = new NotDefaultNullableModel();
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotDefault_Nullable_True_Test1()
        {
            var model = new NotDefaultNullableModel { Id = 1, Code = "Code1", Name = "Name1", IsActive = true };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotDefault_Nullable_False_Test3()
        {
            var model = new NotDefaultNullableModel { Id = 1, Code = "Code1", Name = "Name1" };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }
    }
}
