// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 21:45
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 21:45
// ***********************************************************************
//  <copyright file="ValidationNotNullNullableTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.NotNull;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelAttributeValidationTests.Tests.NotNull
{
    [TestClass]
    public class ValidationNotNullNullableTests
    {
        [TestMethod]
        public void NotNull_False_Test()
        {
            var model = new NotNullNullableModel() { };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotNull_True_Test()
        {
            var model = new NotNullNullableModel()
            {
                Id = 1,
                Code = "Code1",
                Name = "Name1",
                IsActive = true
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotNull_Id_NotSet_True_Test()
        {
            var model = new NotNullNullableModel()
            {
                Code = "Code1",
                Name = "Name1",
                IsActive = true
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotNull_Name_Null_False_Test()
        {
            var model = new NotNullNullableModel()
            {
                Id = 1,
                Code = "Code1",
                IsActive = true
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }
    }
}