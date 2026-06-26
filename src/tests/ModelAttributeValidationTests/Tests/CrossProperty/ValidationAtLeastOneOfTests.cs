#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.CrossProperty;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CrossProperty
{
    [TestClass]
    public class ValidationAtLeastOneOfTests
    {

        [TestMethod]
        public void AtLeastOneOf_BothNull_IsInvalid()
        {
            var model = new ContactModel { Email = null, Phone = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void AtLeastOneOf_EmailSetPhoneNull_IsValid()
        {
            var model = new ContactModel { Email = "test@example.com", Phone = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AtLeastOneOf_EmailNullPhoneSet_IsValid()
        {
            var model = new ContactModel { Email = null, Phone = "+1-555-0100" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AtLeastOneOf_BothSet_IsValid()
        {
            var model = new ContactModel { Email = "test@example.com", Phone = "+1-555-0100" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AtLeastOneOf_WhitespaceEmail_IsInvalid()
        {
            var model = new ContactModel { Email = "   ", Phone = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void AtLeastOneOf_DefaultErrorMessage_ContainsFieldNames()
        {
            var model = new ContactModel { Email = null, Phone = null };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ErrorMessage.Contains("Email"));
            Assert.IsTrue(results[0].ErrorMessage.Contains("Phone"));
        }

        [TestMethod]
        public void AtLeastOneOf_NotesSetButNamedFieldsNull_IsInvalid()
        {
            var model = new MultiFieldContactModel { PrimaryEmail = null, SecondaryEmail = null, Notes = "Some notes" };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void AtLeastOneOf_SecondaryEmailSetNotesIgnored_IsValid()
        {
            var model = new MultiFieldContactModel
            {
                PrimaryEmail = null,
                SecondaryEmail = "backup@example.com", 
                Notes = null
            };
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            Assert.IsTrue(isValid);
        }
    }
}
