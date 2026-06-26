#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.NotDefault;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.NotDefault
{
    [TestClass]
    public class ValidationNotDefaultTests
    {
        [TestMethod]
        public void NotDefault_False_Test()
        {
            var model = new NotDefaultModel();
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotDefault_True_Test()
        {
            var model = new NotDefaultModel { Id = 1, Code = "Code1", Name = "Name1", IsActive = true };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotDefault_True_Test2()
        {
            var model = new NotDefaultModel { Id = 1, Code = "Code1", Name = "Name1", IsActive = true };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }
    }
}
