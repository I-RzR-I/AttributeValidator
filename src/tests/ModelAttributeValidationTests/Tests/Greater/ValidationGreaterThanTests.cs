// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-24 17:29
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-24 17:29
// ***********************************************************************
//  <copyright file="ValidationGreaterThanTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Greater;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelAttributeValidationTests.Tests.Greater
{
    [TestClass]
    public class ValidationGreaterThanTests
    {
        [TestMethod]
        public void GreaterThan_False_Test()
        {
            var model = new GreaterThanModel() { };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_Equals_MinValue_False_Test()
        {
            var model = new GreaterThanModel()
            {
                IntNumber = 0,
                ShortNumber = 1,
                UnSignedShortNumber = 1,
                DecimalNumber = 1,
                FloatNumber = 1,
                DoubleNumber = 1,
                LongNumber = 1
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_False_OnDouble_Test()
        {
            var model = new GreaterThanModel()
            {
                IntNumber = 0,
                ShortNumber = 100,
                UnSignedShortNumber = 100,
                DecimalNumber = 100,
                FloatNumber = 100,
                DoubleNumber = 56.89,
                LongNumber = 100
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_False_OnDecimal_Test()
        {
            var model = new GreaterThanModel()
            {
                IntNumber = 0,
                ShortNumber = 100,
                UnSignedShortNumber = 100,
                DecimalNumber = (decimal)5.68,
                FloatNumber = 100,
                DoubleNumber = 56.91,
                LongNumber = 100
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_MinValue_True_Test()
        {
            var model = new GreaterThanModel()
            {
                IntNumber = 0,
                ShortNumber = 11,
                UnSignedShortNumber = 2,
                DecimalNumber = 6,
                FloatNumber = 7,
                DoubleNumber = 57,
                LongNumber = 6
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }
    }
}