#region U S I N G

using RzR.Validation.Attributes.Attributes.Numeric;

#endregion

namespace ModelAttributeValidationTests.Models.Candidates
{
    public class LatitudeModel
    {
        [ValLatitude]
        public decimal? Latitude { get; set; }
    }

    public class LongitudeModel
    {
        [ValLongitude] 
        public decimal? Longitude { get; set; }
    }
}
