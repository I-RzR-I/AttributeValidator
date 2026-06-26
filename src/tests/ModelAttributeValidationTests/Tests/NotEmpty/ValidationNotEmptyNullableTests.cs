#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.NotEmpty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.NotEmpty
{
    [TestClass]
    public class ValidationNotEmptyNullableTests
    {
        [TestMethod]
        public void NotEmpty_True_Test()
        {
            var model = new NotEmptyNullableModel();
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotEmpty_True_CompleteInfo_Test()
        {
            var model = new NotEmptyNullableModel
            {
                Id = 1,
                IsActive = true,
                GId = Guid.NewGuid(),
                Name = "Name",
                Code = "Code"
            };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotEmpty_True_Name_Null_Test()
        {
            var model = new NotEmptyNullableModel { GId = Guid.NewGuid(), Code = "Code" };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotEmpty_False_Name_Empty_Test()
        {
            var model = new NotEmptyNullableModel { GId = Guid.NewGuid(), Code = "Code", Name = "" };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotEmpty_False_Gid_Null_WithData_Test()
        {
            var model = new NotEmptyNullableModel { Id = 1, Code = "Code1", Name = "Name1", IsActive = true };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }
    }
}
