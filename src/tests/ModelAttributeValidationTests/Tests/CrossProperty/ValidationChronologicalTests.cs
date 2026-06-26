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
    public class ValidationChronologicalTests
    {

        [TestMethod]
        public void Chronological_AllInOrder_IsValid()
        {
            var model = new OrderLifecycleModel
            {
                Created = new DateTime(2024, 1, 1),
                Approved = new DateTime(2024, 2, 1), 
                Shipped = new DateTime(2024, 3, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_SameDateAllowed_IsValid()
        {
            var date = new DateTime(2024, 6, 15);
            var model = new OrderLifecycleModel
            {
                Created = date,
                Approved = date, 
                Shipped = date
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_ApprovedBeforeCreated_IsInvalid()
        {
            var model = new OrderLifecycleModel
            {
                Created = new DateTime(2024, 3, 1), 
                Approved = new DateTime(2024, 1, 1),
                Shipped = new DateTime(2024, 4, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void Chronological_ShippedBeforeApproved_IsInvalid()
        {
            var model = new OrderLifecycleModel
            {
                Created = new DateTime(2024, 1, 1),
                Approved = new DateTime(2024, 4, 1), 
                Shipped = new DateTime(2024, 2, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Chronological_ApprovedNullSkipped_CreatedAndShippedInOrder_IsValid()
        {
            var model = new OrderLifecycleModel
            {
                Created = new DateTime(2024, 1, 1), 
                Approved = null,
                Shipped = new DateTime(2024, 6, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_AllNull_IsValid()
        {
            var model = new OrderLifecycleModel
            {
                Created = null, 
                Approved = null,
                Shipped = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_OnlyCreatedSet_IsValid()
        {
            var model = new OrderLifecycleModel
            {
                Created = new DateTime(2024, 1, 1),
                Approved = null, Shipped = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_CreatedNullApprovedAndShippedInOrder_IsValid()
        {
            var model = new OrderLifecycleModel
            {
                Created = null, 
                Approved = new DateTime(2024, 2, 1), 
                Shipped = new DateTime(2024, 5, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_CreatedNullApprovedAfterShipped_IsInvalid()
        {
            var model = new OrderLifecycleModel
            {
                Created = null, 
                Approved = new DateTime(2024, 5, 1), 
                Shipped = new DateTime(2024, 2, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Chronological_DefaultErrorMessage_ContainsPropertyNames()
        {
            var model = new OrderLifecycleModel
            {
                Created = new DateTime(2024, 3, 1),
                Approved = new DateTime(2024, 1, 1),
                Shipped = new DateTime(2024, 4, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ErrorMessage.Contains("Created"));
            Assert.IsTrue(results[0].ErrorMessage.Contains("Approved"));
            Assert.IsTrue(results[0].ErrorMessage.Contains("Shipped"));
        }

        [TestMethod]
        public void Chronological_NonNullable_StartBeforeEnd_IsValid()
        {
            var model = new DateIntervalModel
            {
                Start = new DateTime(2024, 1, 1), 
                End = new DateTime(2024, 12, 31)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_NonNullable_StartEqualsEnd_IsValid()
        {
            var date = new DateTime(2024, 6, 1);
            var model = new DateIntervalModel { Start = date, End = date };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Chronological_NonNullable_StartAfterEnd_IsInvalid()
        {
            var model = new DateIntervalModel
            {
                Start = new DateTime(2024, 12, 31),
                End = new DateTime(2024, 1, 1)
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }
    }
}
