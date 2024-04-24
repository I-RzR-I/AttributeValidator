// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 22:49
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 22:49
// ***********************************************************************
//  <copyright file="ValidationPositiveTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Positive;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelAttributeValidationTests.Tests.Positive
{
    [TestClass]
    public class ValidationPositiveTests
    {
        [TestMethod]
        public void Positive_False_Test()
        {
            var model = new PositiveModel() { };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_False_Test_1()
        {
            var model = new PositiveModel()
            {
                LongNumber = 0,
                DecimalNumber = 0,
                DoubleNumber = 0D,
                FloatNumber = 0F,
                ShortNumber = 0,
                UnSignedShortNumber = 0,
                IntNumber = 0
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_False_Test_2()
        {
            var model = new PositiveModel()
            {
                LongNumber = 0,
                DecimalNumber = 0,
                DoubleNumber = 0D,
                FloatNumber = 0F,
                ShortNumber = 0,
                UnSignedShortNumber = 0
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_False_Test_3()
        {
            var model = new PositiveModel()
            {
                LongNumber = 1,
                DecimalNumber = 0,
                DoubleNumber = 0D,
                FloatNumber = 0F,
                ShortNumber = 0,
                UnSignedShortNumber = 0
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Positive_True_Test()
        {
            var model = new PositiveModel()
            {
                LongNumber = 1,
                DecimalNumber = 1,
                DoubleNumber = 1D,
                FloatNumber = 1F,
                ShortNumber = 1,
                UnSignedShortNumber = 1
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Positive_True_Test_2()
        {
            var model = new PositiveModel()
            {
                LongNumber = 1,
                DecimalNumber = 1,
                DoubleNumber = 1D,
                FloatNumber = 1F,
                ShortNumber = 1,
                UnSignedShortNumber = 1,
                IntNumber = 0
            };
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            Assert.IsTrue(isValid);
        }
    }
}