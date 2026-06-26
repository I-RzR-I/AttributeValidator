#region U S I N G

using RzR.Validation.Attributes.Attributes.Numeric;

#endregion

namespace ModelAttributeValidationTests.Models.Candidates
{
    public class PercentageModel
    {
        [ValPercentage] 
        public decimal? Value { get; set; }
    }

    public class PercentageFractionModel
    {
        [ValPercentage(true)] 
        public decimal? Value { get; set; }
    }
}
