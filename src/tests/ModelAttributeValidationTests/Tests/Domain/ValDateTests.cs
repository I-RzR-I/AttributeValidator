#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Domain
{
    [TestClass]
    public class ValDateTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void NotFuture_PastDate_IsValid()
        {
            // Arrange
            var model = new NotFutureModel { EventDate = DateTime.UtcNow.AddDays(-1) };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotFuture_FutureDate_IsInvalid()
        {
            // Arrange
            var model = new NotFutureModel { EventDate = DateTime.UtcNow.AddDays(1) };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotFuture_NullableNull_IsInvalid()
        {
            // Arrange
            var model = new NotFutureNullableModel { EventDate = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotFuture_FutureDate_DefaultErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new NotFutureModel { EventDate = DateTime.UtcNow.AddDays(1) };

            // Act
            var (_, results) = Validate(model);

            // Assert
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "EventDate");
        }

        [TestMethod]
        public void NotFuture_FutureDate_DefaultErrorMessageContainsFuture()
        {
            // Arrange
            var model = new NotFutureModel { EventDate = DateTime.UtcNow.AddDays(1) };

            // Act
            var (_, results) = Validate(model);

            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "future");
        }

        [TestMethod]
        public void NotFuture_CustomUserMessage_UsesCustomText()
        {
            // Arrange
            var model = new NotFutureCustomMessageModel { EventDate = DateTime.UtcNow.AddDays(2) };

            // Act
            var (_, results) = Validate(model);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual("Date must not be in the future.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void NotPast_FutureDate_IsValid()
        {
            // Arrange
            var model = new NotPastModel { ScheduledDate = DateTime.UtcNow.AddDays(1) };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotPast_PastDate_IsInvalid()
        {
            // Arrange
            var model = new NotPastModel { ScheduledDate = DateTime.UtcNow.AddDays(-1) };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotPast_NullableNull_IsInvalid()
        {
            // Arrange
            var model = new NotPastNullableModel { ScheduledDate = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NotPast_PastDate_DefaultErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new NotPastModel { ScheduledDate = DateTime.UtcNow.AddDays(-1) };

            // Act
            var (_, results) = Validate(model);

            // Assert
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "ScheduledDate");
        }

        [TestMethod]
        public void MinAge_DobTwentyYearsAgo_IsValid()
        {
            // Arrange
            var dob = DateTime.UtcNow.Date.AddYears(-20);
            var model = new MinAgeModel { DateOfBirth = dob };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MinAge_DobSeventeenYearsAgo_IsInvalid()
        {
            // Arrange
            var dob = DateTime.UtcNow.Date.AddYears(-17).AddDays(1);
            var model = new MinAgeModel { DateOfBirth = dob };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MinAge_DobExactlyEighteenYearsAgo_IsValid()
        {
            // Arrange
            var dob = DateTime.UtcNow.Date.AddYears(-18);
            var model = new MinAgeModel { DateOfBirth = dob };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MinAge_NullableNull_IsInvalid()
        {
            // Arrange
            var model = new MinAgeNullableModel { DateOfBirth = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MinAge_Dob_DefaultErrorMessageContainsMemberName()
        {
            // Arrange
            var dob = DateTime.UtcNow.Date.AddYears(-17).AddDays(1);
            var model = new MinAgeModel { DateOfBirth = dob };

            // Act
            var (_, results) = Validate(model);

            // Assert
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "DateOfBirth");
        }

        [TestMethod]
        public void MinAge_Dob_DefaultErrorMessageContainsMinimumAge()
        {
            // Arrange
            var dob = DateTime.UtcNow.Date.AddYears(-17).AddDays(1);
            var model = new MinAgeModel { DateOfBirth = dob };

            // Act
            var (_, results) = Validate(model);

            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage.ToLowerInvariant(), "minimum age");
        }

        [TestMethod]
        public void MinAge_CustomUserMessage_UsesCustomText()
        {
            // Arrange
            var dob = DateTime.UtcNow.Date.AddYears(-17).AddDays(1);
            var model = new MinAgeCustomMessageModel { DateOfBirth = dob };

            // Act
            var (_, results) = Validate(model);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual("Must be at least 18 years old.", results[0].ErrorMessage);
        }
    }
}
