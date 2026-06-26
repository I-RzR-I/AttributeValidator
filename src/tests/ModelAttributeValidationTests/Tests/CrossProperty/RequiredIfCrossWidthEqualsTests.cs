#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Validation.Attributes.Attributes.Conditional;
using RzR.Validation.Attributes.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.CrossProperty
{
    public class CrossWidthEqualsModel
    {
        public long Code { get; set; }

        [ValRequiredIf(nameof(Code), ValOp.Equals, 1)] 
        public string Detail { get; set; }
    }

    [TestClass]
    public class RequiredIfCrossWidthEqualsTests
    {
        private static bool Validate(object model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(model, context, results, true);
        }

        [TestMethod]
        public void Equals_LongSiblingMatchesIntComparand_ConditionFires_DetailRequired() =>
            Assert.IsFalse(Validate(new CrossWidthEqualsModel { Code = 1, Detail = null }));

        [TestMethod]
        public void Equals_LongSiblingMatchesIntComparand_DetailSupplied_IsValid() 
            => Assert.IsTrue(Validate(new CrossWidthEqualsModel { Code = 1, Detail = "x" }));

        [TestMethod]
        public void Equals_LongSiblingDiffersFromComparand_ConditionDoesNotFire_IsValid()
            => Assert.IsTrue(Validate(new CrossWidthEqualsModel { Code = 2, Detail = null }));
    }
}
