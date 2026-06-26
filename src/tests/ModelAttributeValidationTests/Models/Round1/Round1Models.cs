#region U S I N G

using RzR.Validation.Attributes.Attributes.Common;
using RzR.Validation.Attributes.Attributes.Date;
using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Less;
using System;

#endregion

namespace ModelAttributeValidationTests.Models.Round1
{
    public class GreaterThanLongCrossWidthModel
    {
        [ValGreaterThan(2147483648L)]
        public long Value { get; set; }
    }

    public class LessThanLongCrossWidthModel
    {
        [ValLessThan(2147483648L)] 
        public long Value { get; set; }
    }

    public class AllowedValuesCrossWidthModel
    {
        [ValAllowedValues(1L, 2L, 3L)]
        public int Code { get; set; }
    }

    public class DeniedValuesCrossWidthModel
    {
        [ValDeniedValues(1L, 2L)] 
        public int Code { get; set; }
    }

    public class MinAgeLocalTimeModel
    {
        [ValMinAge(18, false)]
        public DateTime DateOfBirth { get; set; }
    }

    public class MinAgeUtcDefaultModel
    {
        [ValMinAge(18)] 
        public DateTime DateOfBirth { get; set; }
    }

    public enum UserRole
    {
        Guest = 0,
        Member = 1,
        Admin = 2
    }

    public class ValEnumRenamedModel
    {
        [ValEnum(typeof(UserRole), "Status")] 
        public UserRole Status { get; set; }
    }

    public class ValAjaxOnlyRenamedModel
    {
        [ValAjaxOnly] 
        public bool IsAjax { get; set; }
    }
}
