// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 21:00
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 21:00
// ***********************************************************************
//  <copyright file="ValidationNotDefaultNullableTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelAttributeValidationTests.Tests.NotDefault
{
    [TestClass]
    public class ValidationNotDefaultNullableTests
    {
        [TestMethod]
        public void NotDefault_Nullable_False_Test2()
        {
            var model = new Models.NotDefault.NotDefaultNullableModel() { };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotDefault_Nullable_True_Test1()
        {
            var model = new Models.NotDefault.NotDefaultNullableModel()
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
        public void NotDefault_Nullable_False_Test3()
        {
            var model = new Models.NotDefault.NotDefaultNullableModel()
            {
                Id = 1,
                Code = "Code1",
                Name = "Name1"
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }
    }
}