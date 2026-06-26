#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Less;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Greater
{
    public class CrossTypeIntVsFractionalModel
    {
        [ValGreaterThan(1.5)] public int Amount { get; set; }
    }

    public class CrossTypeLessThanModel
    {
        [ValLessThan(2.5)] public int Amount { get; set; }
    }

    [TestClass]
    public class ValueComparerCrossTypeTests
    {
        private static bool Validate(object model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(model, context, results, true);
        }

        [TestMethod]
        public void GreaterThan_IntValueAboveFractionalThreshold_IsValid() =>
            Assert.IsTrue(Validate(new CrossTypeIntVsFractionalModel { Amount = 2 }));

        [TestMethod]
        public void GreaterThan_IntValueBelowFractionalThreshold_IsInvalid() =>
            Assert.IsFalse(Validate(new CrossTypeIntVsFractionalModel { Amount = 1 }));

        [TestMethod]
        public void LessThan_IntValueBelowFractionalThreshold_IsValid() =>
            Assert.IsTrue(Validate(new CrossTypeLessThanModel { Amount = 2 }));

        [TestMethod]
        public void LessThan_IntValueAboveFractionalThreshold_IsInvalid() =>
            Assert.IsFalse(Validate(new CrossTypeLessThanModel { Amount = 3 }));
    }
}
