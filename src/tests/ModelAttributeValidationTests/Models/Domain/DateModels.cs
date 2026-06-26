#region U S I N G

using RzR.Validation.Attributes.Attributes.Date;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Domain
{

    public class NotFutureModel
    {
        [ValNotFuture] 
        public DateTime EventDate { get; set; }
    }

    public class NotFutureNullableModel
    {
        [ValNotFuture] 
        public DateTime? EventDate { get; set; }
    }

    public class NotFutureCustomMessageModel
    {
        [ValNotFuture(userMessage: "Date must not be in the future.")] 
        public DateTime EventDate { get; set; }
    }

    public class NotPastModel
    {
        [ValNotPast]
        public DateTime ScheduledDate { get; set; }
    }

    public class NotPastNullableModel
    {
        [ValNotPast] 
        public DateTime? ScheduledDate { get; set; }
    }

    public class MinAgeModel
    {
        [ValMinAge(18)] 
        public DateTime DateOfBirth { get; set; }
    }

    public class MinAgeNullableModel
    {
        [ValMinAge(18)]
        public DateTime? DateOfBirth { get; set; }
    }

    public class MinAgeCustomMessageModel
    {
        [ValMinAge(18, userMessage: "Must be at least 18 years old.")]
        public DateTime DateOfBirth { get; set; }
    }
}
