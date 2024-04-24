// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 20:42
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 20:42
// ***********************************************************************
//  <copyright file="ValidationNotDefaultTests.cs" company="">
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
    public class ValidationNotDefaultTests
    {
        [TestMethod]
        public void NotDefault_False_Test()
        {
            var model = new Models.NotDefault.NotDefaultModel() { };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotDefault_True_Test()
        {
            var model = new Models.NotDefault.NotDefaultModel()
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
        public void NotDefault_True_Test2()
        {
            var model = new Models.NotDefault.NotDefaultModel()
            {
                Id = 1,
                Code = "Code1",
                Name = "Name1"
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }
    }
}