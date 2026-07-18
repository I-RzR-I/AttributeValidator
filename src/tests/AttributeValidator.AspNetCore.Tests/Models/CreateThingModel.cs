using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Require;

namespace AttributeValidator.AspNetCore.Tests.Models;

public class CreateThingModel
{
    [ValRequiredNotEmpty]
    public string Name { get; set; } = string.Empty;

    [ValGreaterThan(0)]
    public int Qty { get; set; }
}
