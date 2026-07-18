# Usage Guide: RzR.Validation.Attributes.Mvvm

`RzR.Validation.Attributes.Mvvm` connects the core `RzR.Validation.Attributes` attributes to `INotifyDataErrorInfo`, the .NET interface most MVVM frameworks use for asynchronous, property-level UI validation.

At the center of the package is one base class, `ValidatableObservableObject`, which combines `INotifyPropertyChanged` and `INotifyDataErrorInfo`. Derive your view-model from it, decorate the properties you care about with the core `Val*` attributes, and each property change gets validated as it happens, with errors reported back to the UI.

The package targets `netstandard2.0` and doesn't depend on WPF, WinUI, MAUI, or Avalonia — it only needs `System.ComponentModel` and `System.ComponentModel.DataAnnotations`, both part of the base class libraries. Any framework that binds to `INotifyDataErrorInfo` works out of the box:

- WPF
- WinUI 3
- .NET MAUI
- Avalonia

---

## Install

```powershell
dotnet add package RzR.Validation.Attributes.Mvvm
```

You don't need to install the core package separately — it comes along as a transitive dependency. Reference the `Val*` attribute namespaces directly in your view-model (as shown below) and those types are already there.

**Target framework:** netstandard2.0.

---

## Define a view-model

Derive from `ValidatableObservableObject`, decorate each property with whatever core attributes it needs, and call `SetProperty` from the setter. `SetProperty` sets the backing field, raises `PropertyChanged`, and validates that one property.

```csharp
using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Identity;
using RzR.Validation.Attributes.Attributes.Require;
using RzR.Validation.Attributes.Mvvm;

public class PersonViewModel : ValidatableObservableObject
{
    private string _name;

    [ValRequiredNotEmpty]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _email;

    [ValEmail]
    public string Email
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
```

Any core `Val*` attribute works here — including custom messages (`[ValEmail("Enter a valid email address.")]`) and stacking more than one attribute on the same property.

---

## WPF: binding to validation errors

WPF needs `ValidatesOnNotifyDataErrors` set (it defaults to `true` in modern WPF, but it's shown explicitly below for clarity), and `NotifyOnValidationError` is worth adding if you want the `Validation.Error` routed event to fire — for a sound, a log entry, whatever your app needs.

```xml
<TextBox Text="{Binding Name, UpdateSourceTrigger=PropertyChanged,
                         ValidatesOnNotifyDataErrors=True,
                         NotifyOnValidationError=True}" />
```

`UpdateSourceTrigger=PropertyChanged` matters here. Without it, the bound property — and therefore validation — only updates once the `TextBox` loses focus, which delays error feedback.

WinUI 3, .NET MAUI, and Avalonia don't need any of this: their bindings pick up `INotifyDataErrorInfo` on their own once a control is bound to a property on a `ValidatableObservableObject`. Each framework's default `TextBox`/`Entry` control shows its own validation error template without extra configuration.

---

## Reading errors in code

`ValidatableObservableObject` exposes `HasErrors`, `GetErrors(string propertyName)`, and the `ErrorsChanged` event, so you can react to validation state outside of XAML — say, to enable or disable a Save command.

```csharp
var viewModel = new PersonViewModel();

viewModel.ErrorsChanged += (sender, args) =>
{
    var errors = viewModel.GetErrors(args.PropertyName).Cast<string>();
    Console.WriteLine($"{args.PropertyName}: {string.Join("; ", errors)}");
};

viewModel.Email = "not-an-email";

bool hasErrors = viewModel.HasErrors;                 // true
var emailErrors = viewModel.GetErrors("Email");        // the ValEmail error message
var allErrors = viewModel.GetErrors(null);              // every error across every property
```

`GetErrors` returns an empty enumerable, never `null`, when a property has no errors — so you can enumerate the result directly without a null check first.

---

## Submit-time validation

`SetProperty` only validates the property that just changed. Class-level and cross-property attributes — `ValRequiredIf`, `ValAtLeastOneOf`, `ValMutuallyExclusive`, `ValExactlyOneOf` — don't run through it, because those rules need to see the whole object, not one property in isolation.

That's what `ValidateAll()` is for. Call it before you save or submit. Under the hood it runs `Validator.TryValidateObject` with `validateAllProperties: true`, covering property-level, class-level, and cross-property attributes in a single pass, refreshing the error store, and raising `ErrorsChanged` for every property whose error state changed.

```csharp
public void Save(PersonViewModel viewModel)
{
    if (!viewModel.ValidateAll())
    {
        // viewModel.HasErrors is true; bound controls already show
        // the updated per-property errors via ErrorsChanged.
        return;
    }

    // viewModel is fully valid, including any class-level or
    // cross-property rules — proceed with the save.
}
```

Object-level validation results — errors with no property attached, like a failed `ValAtLeastOneOf` on the class itself — get stored under an empty-string property name. You'll see them when you call `GetErrors(null)` or `GetErrors(string.Empty)`.

---

## Notes

- A property only validates if it carries at least one core `Val*` attribute. `ValidatableObservableObject` doesn't add implicit rules of its own — it runs whatever `System.ComponentModel.DataAnnotations.Validator` finds on the property or class.
- Error messages come from the attributes themselves. Override the default text with each attribute's `userMessage` constructor parameter (or the `ErrorMessage` property for the params-based and class-targeted attributes). `docs/usage.md` has the full attribute reference and message customization rules.
- Cross-property and class-targeted attributes (`ValRequiredIf`, `ValRequiredUnless`, `ValCompareProperty`, `ValAtLeastOneOf`, `ValMutuallyExclusive`, `ValExactlyOneOf`, `ValChronological`) only run through `ValidateAll()` — per-property `SetProperty` validation skips them.
