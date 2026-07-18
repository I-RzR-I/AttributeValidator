using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Identity;
using RzR.Validation.Attributes.Attributes.Require;
using RzR.Validation.Attributes.Mvvm;

namespace AttributeValidator.Mvvm.Tests.ViewModels;

public class PersonViewModel : ValidatableObservableObject
{
    private string? _name;

    [ValRequiredNotEmpty]
    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string? _email;

    [ValEmail]
    public string? Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private int _age;

    [ValGreaterThan(0)]
    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }
}