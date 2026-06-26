#region U S I N G

using RzR.Validation.Attributes.Attributes.Common;

#endregion

namespace ModelAttributeValidationTests.Models.Ajax
{
    public class AjaxModel
    {
        [ValAjaxOnly] 
        public bool IsAjax { get; set; }
    }
}
