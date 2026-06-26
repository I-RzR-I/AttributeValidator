
> **Note** Targets net4.5 (net45), netstandard1.1, and netstandard2.0.

| Name     | Details |
|----------|----------|
| RzR.Validation.Attributes | [![NuGet Version](https://img.shields.io/nuget/v/RzR.Validation.Attributes.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/RzR.Validation.Attributes/) [![Nuget Downloads](https://img.shields.io/nuget/dt/RzR.Validation.Attributes.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/RzR.Validation.Attributes)|

<details>

  <summary>Old version</summary>
  
| Name     | Details |
|----------|----------|
| AttributeValidator | [![NuGet Version](https://img.shields.io/nuget/v/AttributeValidator.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/AttributeValidator/) [![Nuget Downloads](https://img.shields.io/nuget/dt/AttributeValidator.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/AttributeValidator)|

</details>

<br />

RzR.Validation.Attributes is a .NET library that extends `System.ComponentModel.DataAnnotations` with ~58 ready-to-use `ValidationAttribute` subclasses covering presence checks, comparisons, string rules, date/time constraints, format patterns, identity validation, collection guards, conditional requirements, and object-level cross-property rules. Every attribute plugs directly into the standard `Validator` API - no extra infrastructure needed.

```powershell
Install-Package RzR.Validation.Attributes
```

## Features at a glance

| Category | Count | Example attributes |
| --- | --- | --- |
| Presence / Required | 6 | `ValRequiredNotNull`, `ValRequiredNotEmpty`, `ValRequiredNotDefault`, `ValRequiredPositive`, `ValNotWhiteSpace`, `ValGuidNotEmpty` |
| Comparison | 7 | `ValGreaterThan`, `ValGreaterThanOrEqual`, `ValLessThan`, `ValLessThanOrEqual`, `ValBetween`, `ValEqual`, `ValNotEqual` |
| Collections & sets | 3 | `ValCollectionNotEmpty`, `ValAllowedValues`, `ValDeniedValues` |
| Conditional & cross-property | 3 | `ValRequiredIf`, `ValRequiredUnless`, `ValCompareProperty` |
| Object-level (class-targeted) | 4 | `ValAtLeastOneOf`, `ValMutuallyExclusive`, `ValExactlyOneOf`, `ValChronological` |
| Date & time | 3 | `ValNotFuture`, `ValNotPast`, `ValMinAge` |
| Numeric | 5 | `ValDecimalPrecision`, `ValMultipleOf`, `ValPercentage`, `ValLatitude`, `ValLongitude` |
| String | 11 | `ValLengthRange`, `ValMinLength`, `ValMaxLength`, `ValExactLength`, `ValStartsWith`, `ValEndsWith`, `ValContains`, `ValRegex`, `ValAlpha`, `ValAlphaNumeric`, `ValNumericString` |
| Format | 6 | `ValIpAddress`, `ValHexColor`, `ValBase64`, `ValPhoneE164`, `ValIban`, `ValColorName` |
| Identity & contact | 8 | `ValEmail`, `ValUrl`, `ValCreditCard`, `ValUsername`, `ValCountryCode`, `ValCultureCode`, `ValSlug`, `ValPostalCode` (60+ countries) |
| Enum & misc | 2 | `ValEnum`, `ValAjaxOnly` |

Full attribute reference, constructor signatures, and edge-case notes are in [docs/usage.md](docs/usage.md).

## Quick start

```csharp
using RzR.Validation.Attributes.Attributes.Require;
using RzR.Validation.Attributes.Attributes.String;
using RzR.Validation.Attributes.Attributes.Greater;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class CreateOrderRequest
{
    [ValRequiredNotNull]
    [ValRequiredNotEmpty]
    public string CustomerName { get; set; }

    [ValGreaterThan(0)]
    public decimal Amount { get; set; }

    [ValMinLength(2)]
    [ValMaxLength(100)]
    public string Notes { get; set; }
}

// Validate using the standard DataAnnotations API:
var model = new CreateOrderRequest { CustomerName = "Acme", Amount = 49.99m };
var results = new List<ValidationResult>();
bool isValid = Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
```

## Good to know

- **Custom error messages.** Every attribute accepts an optional trailing `string userMessage` constructor parameter that overrides the default message.
- **Null is invalid by design.** All non-presence attributes (comparisons, string rules, format, numeric, etc.) reject `null`. Each rule is self-contained. When a field is truly optional, pair the non-presence attribute with a presence attribute only if the field is present - or handle nullability in your model before validation.
- **Object-level attributes require `validateAllProperties: true`.** The class-targeted attributes (`ValAtLeastOneOf`, `ValMutuallyExclusive`, `ValExactlyOneOf`, `ValChronological`) and the cross-property conditional attributes (`ValRequiredIf`, `ValRequiredUnless`, `ValCompareProperty`) only execute when you call `Validator.TryValidateObject(..., validateAllProperties: true)`.
- **`ValAjaxOnly` is not a security control.** It validates a caller-supplied `bool` flag only. Real AJAX enforcement must be done server-side via an action filter or middleware that reads the `X-Requested-With` header.

## Content

1. [USING](docs/usage.md)
2. [CHANGELOG](docs/CHANGELOG.md)
3. [BRANCH-GUIDE](docs/branch-guide.md)
