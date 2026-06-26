#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CrossProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CrossProperty
{
    [TestClass]
    public class ValidationComparePropertyTests
    {

        [TestMethod]
        public void CompareProperty_DateTime_EndAfterStart_IsValid()
        {
            var model = new DateRangeModel
            {
                StartDate = new DateTime(2024, 1, 1), 
                EndDate = new DateTime(2024, 6, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CompareProperty_DateTime_EndBeforeStart_IsInvalid()
        {
            var model = new DateRangeModel
            {
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2024, 1, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void CompareProperty_DateTime_EndEqualStart_GreaterThan_IsInvalid()
        {
            var date = new DateTime(2024, 3, 15);
            var model = new DateRangeModel
            {
                StartDate = date, EndDate = date
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CompareProperty_DateTime_EndEqualStart_GreaterThanOrEqual_IsValid()
        {
            var date = new DateTime(2024, 3, 15);
            var model = new DateRangeOrEqualModel
            {
                StartDate = date, EndDate = date
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CompareProperty_DateTime_EndAfterStart_GreaterThanOrEqual_IsValid()
        {
            var model = new DateRangeOrEqualModel
            {
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 12, 31)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CompareProperty_DateTime_EndBeforeStart_GreaterThanOrEqual_IsInvalid()
        {
            var model = new DateRangeOrEqualModel
            {
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2024, 1, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CompareProperty_StringEquals_BothMatch_IsValid()
        {
            var model = new PasswordConfirmModel
            {
                Password = "Secret123!",
                ConfirmPassword = "Secret123!"
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CompareProperty_StringEquals_Mismatch_IsInvalid()
        {
            var model = new PasswordConfirmModel
            {
                Password = "Secret123!", 
                ConfirmPassword = "Different!"
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void CompareProperty_StringEquals_BothNull_IsValid()
        {
            var model = new PasswordConfirmModel
            {
                Password = null, 
                ConfirmPassword = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CompareProperty_StringEquals_OneNull_OtherSet_IsInvalid()
        {
            var model = new PasswordConfirmModel
            {
                Password = "Secret123!", 
                ConfirmPassword = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CompareProperty_DateTime_DefaultErrorMessage_ContainsMemberNames()
        {
            var model = new DateRangeModel
            {
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2024, 1, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ErrorMessage.Contains("EndDate"));
            Assert.IsTrue(results[0].ErrorMessage.Contains("StartDate"));
        }

        [TestMethod]
        public void CompareProperty_CustomMessage_InvalidComparison_ReturnsCustomMessage()
        {
            var model = new ComparePropertyCustomMessageModel
            {
                Min = 10, 
                Max = 5
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual("Max must exceed Min.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void CompareProperty_CustomMessage_ValidComparison_IsValid()
        {
            var model = new ComparePropertyCustomMessageModel
            {
                Min = 5, 
                Max = 10
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }
    }
}
