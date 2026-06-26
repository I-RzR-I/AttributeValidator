#region U S I N G

using RzR.Validation.Attributes.Attributes.Greater;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanTimeSpanModel
    {
        [ValGreaterThan("01:00:00")] 
        public TimeSpan Duration { get; set; }
    }
}
