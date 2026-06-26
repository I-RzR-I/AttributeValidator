#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Round1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Round1
{
    [TestClass]
    public class Round1RegressionTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void GreaterThan_Long_AboveIntMaxComparand_ValueOneAbove_IsValid()
        {
            // Arrange
            var model = new GreaterThanLongCrossWidthModel { Value = 2147483649L };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GreaterThan_Long_AboveIntMaxComparand_ValueBelowThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanLongCrossWidthModel { Value = 2147483647L };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GreaterThan_Long_AboveIntMaxComparand_ValueEqualToThreshold_IsInvalid()
        {
            // Arrange
            var model = new GreaterThanLongCrossWidthModel { Value = 2147483648L };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void LessThan_Long_AboveIntMaxComparand_ValueOneBelowThreshold_IsValid()
        {
            // Arrange
            var model = new LessThanLongCrossWidthModel { Value = 2147483647L };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void LessThan_Long_AboveIntMaxComparand_ValueAboveThreshold_IsInvalid()
        {
            // Arrange
            var model = new LessThanLongCrossWidthModel { Value = 2147483649L };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void AllowedValues_CrossWidth_IntValueMatchesLongEntry_IsValid()
        {
            // Arrange
            var model = new AllowedValuesCrossWidthModel { Code = 2 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AllowedValues_CrossWidth_IntValueNotInLongSet_IsInvalid()
        {
            // Arrange
            var model = new AllowedValuesCrossWidthModel { Code = 9 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void DeniedValues_CrossWidth_IntValueMatchesDeniedLongEntry_IsInvalid()
        {
            // Arrange
            var model = new DeniedValuesCrossWidthModel { Code = 1 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void DeniedValues_CrossWidth_IntValueNotInDeniedSet_IsValid()
        {
            // Arrange
            var model = new DeniedValuesCrossWidthModel { Code = 5 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MinAge_UseUtcFalse_DobTwentyYearsAgo_IsValid()
        {
            // Arrange
            var dob = DateTime.Now.Date.AddYears(-20);
            var model = new MinAgeLocalTimeModel { DateOfBirth = dob };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void MinAge_UseUtcFalse_DobTenYearsAgo_IsInvalid()
        {
            // Arrange
            var dob = DateTime.Now.Date.AddYears(-10);
            var model = new MinAgeLocalTimeModel { DateOfBirth = dob };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MinAge_UseUtcTrue_Default_DobTwentyYearsAgo_IsValid()
        {
            // Arrange
            var dob = DateTime.UtcNow.Date.AddYears(-20);
            var model = new MinAgeUtcDefaultModel { DateOfBirth = dob };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValEnum_Renamed_DefinedMember_IsValid()
        {
            // Arrange
            var model = new ValEnumRenamedModel { Status = UserRole.Admin };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValEnum_Renamed_UndefinedCastValue_IsInvalid()
        {
            // Arrange
            var model = new ValEnumRenamedModel { Status = (UserRole)999 };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValEnum_Renamed_ZeroValueMember_IsValid()
        {
            // Arrange
            var model = new ValEnumRenamedModel { Status = UserRole.Guest };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValAjaxOnly_Renamed_IsAjaxTrue_IsValid()
        {
            // Arrange
            var model = new ValAjaxOnlyRenamedModel { IsAjax = true };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValAjaxOnly_Renamed_IsAjaxFalse_IsInvalid()
        {
            // Arrange
            var model = new ValAjaxOnlyRenamedModel { IsAjax = false };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
