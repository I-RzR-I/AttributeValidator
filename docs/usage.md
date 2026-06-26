# Usage Guide

RzR.Validation.Attributes is a .NET library that extends `System.ComponentModel.DataAnnotations` with ~58 `ValidationAttribute` subclasses. Every attribute plugs directly into the standard `Validator` API.

**NuGet package:**

```powershell
Install-Package RzR.Validation.Attributes
```

**Target frameworks:** net4.5, netstandard1.1, netstandard2.0.

---

## Getting started

Decorate your model properties with the attributes you need, then call `Validator.TryValidateObject`. This is the standard `System.ComponentModel.DataAnnotations` validation pipeline - no extra infrastructure or configuration is required.

```csharp
using RzR.Validation.Attributes.Attributes.Require;
using RzR.Validation.Attributes.Attributes.String;
using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Format;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class CreateAccountRequest
{
    [ValRequiredNotNull]
    [ValRequiredNotEmpty]
    public string Username { get; set; }

    [ValEmail]
    public string Email { get; set; }

    [ValGreaterThan(0)]
    public decimal Balance { get; set; }
}

// Validate the model:
var model = new CreateAccountRequest { Username = "alice", Email = "alice@example.com", Balance = 10m };
var results = new List<ValidationResult>();
bool isValid = Validator.TryValidateObject(model,
    new ValidationContext(model),
    results, validateAllProperties: true);
```

You can mix these attributes with built-in `System.ComponentModel.DataAnnotations` attributes such as `[Required]`, `[Range]`, and `[StringLength]` on the same property.

---

## Conventions

### Custom error message pattern

Every attribute accepts an optional custom error message. For most attributes it is the last constructor parameter, named `userMessage`:

```csharp
[ValRequiredNotNull("Username is required.")]
[ValGreaterThan(0, "Amount must be greater than zero.")]
public string Username { get; set; }
```

For the params-based attributes (`ValAllowedValues` and `ValDeniedValues`) the constructor takes `params object[]`, so a trailing string cannot be unambiguously distinguished from a value. Set the inherited `ErrorMessage` property instead:

```csharp
[ValAllowedValues("pending", "active", "closed") { ErrorMessage = "Status must be pending, active, or closed." }]
public string Status { get; set; }
```

For the object-level (class-targeted) attributes (`ValAtLeastOneOf`, `ValMutuallyExclusive`, `ValExactlyOneOf`, `ValChronological`) use the same `ErrorMessage` property pattern.

### Null is invalid by default

Every non-presence attribute treats null as an invalid value. Each rule is self-contained: a null value can never pass a comparison, string, format, or numeric check. When a field is genuinely optional, design your model so that the property carries a nullable type and only apply non-presence attributes when you know the value is present, or pair them with a presence check and control which attributes run.

### Cross-property and object-level attributes require `validateAllProperties: true`

The conditional attributes (`ValRequiredIf`, `ValRequiredUnless`, `ValCompareProperty`) and the class-targeted attributes (`ValAtLeastOneOf`, `ValMutuallyExclusive`, `ValExactlyOneOf`, `ValChronological`) rely on `ValidationContext` to inspect sibling properties or the object instance. They only execute when you call:

```csharp
Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
```

They do not fire during per-property MVC model binding.

### ValOp enum

The conditional and cross-property attributes use the `ValOp` enum (namespace `RzR.Validation.Attributes.Common`) to express comparison operators:

| Member | Meaning |
| --- | --- |
| `ValOp.Equals` | Left == right |
| `ValOp.NotEquals` | Left != right |
| `ValOp.GreaterThan` | Left > right |
| `ValOp.GreaterThanOrEqual` | Left >= right |
| `ValOp.LessThan` | Left < right |
| `ValOp.LessThanOrEqual` | Left <= right |

---

## Presence / Required

**Namespace:** `RzR.Validation.Attributes.Attributes.Require`

These attributes check whether a value is meaningfully present. They are the foundation for optional/required field semantics. Use them alone or in combination.

| Attribute | What passes |
| --- | --- |
| `ValRequiredNotNull` | Value is not null |
| `ValRequiredNotEmpty` | Not null; strings not null/whitespace; `Guid` not `Guid.Empty` |
| `ValRequiredNotDefault` | Not null; value types not equal to `default(T)` |
| `ValRequiredPositive` | Numeric value > 0 (null fails) |
| `ValNotWhiteSpace` | Strings: not null/empty/whitespace; non-strings: not null |
| `ValGuidNotEmpty` | `Guid` or parseable string not equal to `Guid.Empty` |

**Constructors:**

```csharp
ValRequiredNotNullAttribute(string userMessage = null)
ValRequiredNotEmptyAttribute(string userMessage = null)
ValRequiredNotDefaultAttribute(string userMessage = null)
ValRequiredPositiveAttribute(string userMessage = null)
ValNotWhiteSpaceAttribute(string userMessage = null)
ValGuidNotEmptyAttribute(string userMessage = null)
```

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Require;
using System.ComponentModel.DataAnnotations;

public class RegistrationRequest
{
    [ValRequiredNotNull]
    [ValRequiredNotEmpty]
    public string FullName { get; set; }

    [ValRequiredPositive]
    [ValRequiredNotDefault]
    public int Age { get; set; }

    [ValGuidNotEmpty]
    public Guid SessionId { get; set; }

    [ValNotWhiteSpace]
    public string Nickname { get; set; }
}
```

---

## Comparison

**Namespaces:**

- `RzR.Validation.Attributes.Attributes.Greater` - `ValGreaterThan`, `ValGreaterThanOrEqual`
- `RzR.Validation.Attributes.Attributes.Less` - `ValLessThan`, `ValLessThanOrEqual`
- `RzR.Validation.Attributes.Attributes.Range` - `ValBetween`
- `RzR.Validation.Attributes.Attributes.Common` - `ValEqual`, `ValNotEqual`

These attributes compare the decorated value against a fixed constant. They work on any numeric type and on any other type that supports `IComparable`. Numeric values of different widths (e.g. `int` vs `long`) compare by value. Null always fails.

**Constructors:**

```csharp
ValGreaterThanAttribute(object greaterThan, string userMessage = null)
ValGreaterThanOrEqualAttribute(object greaterThanOrEqual, string userMessage = null)
ValLessThanAttribute(object lessThan, string userMessage = null)
ValLessThanOrEqualAttribute(object lessThanOrEqual, string userMessage = null)
ValBetweenAttribute(object min, object max, bool inclusive = true, string userMessage = null)
ValEqualAttribute(object comparand, string userMessage = null)
ValNotEqualAttribute(object comparand, string userMessage = null)
```

`ValBetween` defaults to inclusive bounds (`[min, max]`). Pass `inclusive: false` for exclusive bounds `(min, max)`.

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Greater;
using RzR.Validation.Attributes.Attributes.Less;
using RzR.Validation.Attributes.Attributes.Range;
using RzR.Validation.Attributes.Attributes.Common;
using System.ComponentModel.DataAnnotations;

public class ProductRequest
{
    [ValGreaterThan(0)]
    public decimal Price { get; set; }

    [ValGreaterThanOrEqual(1)]
    public int Quantity { get; set; }

    [ValLessThan(1000)]
    public int Weight { get; set; }

    [ValLessThanOrEqual(100)]
    public int DiscountPercent { get; set; }

    [ValBetween(1, 12)]          // inclusive: default true
    public int Month { get; set; }

    [ValBetween(0.0, 1.0, inclusive: false)]
    public double Ratio { get; set; }

    [ValNotEqual(0)]
    public long ExternalId { get; set; }

    [ValEqual("confirmed")]
    public string ExpectedStatus { get; set; }
}
```

---

## Collections and allowed/denied values

**Namespaces:**

- `RzR.Validation.Attributes.Attributes.Collection` - `ValCollectionNotEmpty`
- `RzR.Validation.Attributes.Attributes.Common` - `ValAllowedValues`, `ValDeniedValues`

**Constructors:**

```csharp
ValCollectionNotEmptyAttribute(string userMessage = null)
ValAllowedValuesAttribute(params object[] allowedValues)
ValDeniedValuesAttribute(params object[] deniedValues)
```

`ValCollectionNotEmpty` works on `ICollection`, `IEnumerable`, and strings. For strings it uses `string.IsNullOrEmpty`; for `ICollection` it checks `Count > 0`; for other enumerables it advances the enumerator once.

`ValAllowedValues` and `ValDeniedValues` use value-aware equality (same as `object.Equals`). Null may appear as an explicit entry. To set a custom error message, use the `ErrorMessage` property - you cannot use a trailing `userMessage` parameter because the constructor is `params`-based.

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Collection;
using RzR.Validation.Attributes.Attributes.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class FilterRequest
{
    [ValCollectionNotEmpty]
    public List<int> Ids { get; set; }

    [ValAllowedValues("pending", "active", "closed")]
    public string Status { get; set; }

    [ValDeniedValues("root", "admin", "system")]
    public string Username { get; set; }
}
```

Custom message for allowed/denied values:

```csharp
[ValAllowedValues("US", "CA", "GB") { ErrorMessage = "Country must be US, CA, or GB." }]
public string Country { get; set; }
```

---

## Conditional and cross-property

**Namespace:** `RzR.Validation.Attributes.Attributes.Conditional`

These attributes read sibling property values at runtime via `ValidationContext`. They only run when you call `Validator.TryValidateObject(..., validateAllProperties: true)`. They do not fire during per-property MVC model binding.

**Constructors:**

```csharp
ValRequiredIfAttribute(string otherProperty, ValOp op, object comparand, string userMessage = null)
ValRequiredUnlessAttribute(string otherProperty, ValOp op, object comparand, string userMessage = null)
ValComparePropertyAttribute(string otherProperty, ValOp op, string userMessage = null)
```

- `ValRequiredIf` - the decorated property must be populated when `otherProperty op comparand` is true.
- `ValRequiredUnless` - the decorated property must be populated when `otherProperty op comparand` is false (i.e., required unless the condition holds).
- `ValCompareProperty` - the decorated property must satisfy `thisValue op otherPropertyValue`.

When the sibling property cannot be found or the values are not comparable, ordering comparisons fail closed (validation error returned).

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Conditional;
using RzR.Validation.Attributes.Common;
using System;
using System.ComponentModel.DataAnnotations;

// ValCompareProperty: EndDate must be >= StartDate
// ValRequiredIf: ShippingAddress required when OrderType equals "delivery"
public class OrderRequest
{
    public DateTime StartDate { get; set; }

    [ValCompareProperty("StartDate", ValOp.GreaterThanOrEqual)]
    public DateTime EndDate { get; set; }

    public string OrderType { get; set; }

    [ValRequiredIf("OrderType", ValOp.Equals, "delivery")]
    public string ShippingAddress { get; set; }

    // Required unless IsDraft is true
    [ValRequiredUnless("IsDraft", ValOp.Equals, true)]
    public string CustomerName { get; set; }

    public bool IsDraft { get; set; }
}
```

```csharp
// Remember to pass validateAllProperties: true
var results = new List<ValidationResult>();
bool isValid = Validator.TryValidateObject(
    order,
    new ValidationContext(order),
    results,
    validateAllProperties: true);
```

---

## Object-level (class-targeted)

**Namespace:** `RzR.Validation.Attributes.Attributes.Object`

These attributes are placed on the class declaration, not on individual properties. They inspect named properties via reflection at validation time. They run only when you call `Validator.TryValidateObject(..., validateAllProperties: true)`.

All four support `AllowMultiple = true`, so you can declare multiple independent groups on the same class.

**Constructors:**

```csharp
ValAtLeastOneOfAttribute(params string[] propertyNames)
ValMutuallyExclusiveAttribute(params string[] propertyNames)
ValExactlyOneOfAttribute(params string[] propertyNames)
ValChronologicalAttribute(params string[] propertyNames)
```

| Attribute | Passes when |
| --- | --- |
| `ValAtLeastOneOf` | At least one named property is populated |
| `ValMutuallyExclusive` | At most one named property is populated |
| `ValExactlyOneOf` | Exactly one named property is populated |
| `ValChronological` | Named properties form a non-decreasing sequence (null values skipped) |

`ValChronological` is designed for `DateTime` and `DateTime?` properties but works on any type supported by the internal `ValueComparer`. If a consecutive pair cannot be compared, validation fails closed.

To set a custom error message on these attributes, set the `ErrorMessage` property:

```csharp
[ValAtLeastOneOf("Email", "Phone") { ErrorMessage = "Provide at least one contact method." }]
```

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Object;
using System;
using System.ComponentModel.DataAnnotations;

[ValAtLeastOneOf("Email", "Phone")]
[ValMutuallyExclusive("CreditCard", "BankAccount")]
[ValExactlyOneOf("PayByCard", "PayByInvoice", "PayByWallet")]
[ValChronological("OrderDate", "ShippingDate", "DeliveryDate")]
public class CheckoutRequest
{
    public string Email { get; set; }
    public string Phone { get; set; }

    public string CreditCard { get; set; }
    public string BankAccount { get; set; }

    public string PayByCard { get; set; }
    public string PayByInvoice { get; set; }
    public string PayByWallet { get; set; }

    public DateTime? OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
}
```

---

## Date and time

**Namespace:** `RzR.Validation.Attributes.Attributes.Date`

These attributes accept `DateTime` and (where noted) `DateTimeOffset`. Null always fails. When `useUtc` is `true` (the default), comparisons are made against `DateTime.UtcNow` or `DateTimeOffset.UtcNow`. Pass `useUtc: false` to compare against local time.

**Constructors:**

```csharp
ValNotFutureAttribute(bool useUtc = true, string userMessage = null)
ValNotPastAttribute(bool useUtc = true, string userMessage = null)
ValMinAgeAttribute(int minAgeYears, bool useUtc = true, string userMessage = null)
```

`ValNotFuture` - value must be <= now (date is in the past or present).
`ValNotPast` - value must be >= now (date is in the future or present).
`ValMinAge` - treats the value as a date of birth; passes when the person has reached `minAgeYears` whole years.

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Date;
using System;
using System.ComponentModel.DataAnnotations;

public class EventRequest
{
    [ValNotFuture]
    public DateTime CreatedAt { get; set; }

    [ValNotPast]
    public DateTime ScheduledFor { get; set; }

    [ValMinAge(18)]
    public DateTime DateOfBirth { get; set; }

    // Use local time instead of UTC:
    [ValNotFuture(useUtc: false)]
    public DateTime LocalTimestamp { get; set; }
}
```

---

## Numeric

**Namespace:** `RzR.Validation.Attributes.Attributes.Numeric`

All numeric attributes reject null and values that cannot be converted to the target numeric type.

**Constructors:**

```csharp
ValDecimalPrecisionAttribute(int precision, int scale, string userMessage = null)
ValMultipleOfAttribute(object factor, string userMessage = null)
ValPercentageAttribute(bool asFraction = false, string userMessage = null)
ValLatitudeAttribute(string userMessage = null)
ValLongitudeAttribute(string userMessage = null)
```

| Attribute | What passes |
| --- | --- |
| `ValDecimalPrecision(p, s)` | Value fits in SQL `DECIMAL(p, s)`: at most `p` total significant digits, at most `s` decimal places |
| `ValMultipleOf(factor)` | `value % factor == 0`; a zero factor always fails |
| `ValPercentage` | `[0, 100]` when `asFraction` is false; `[0, 1]` when `asFraction` is true |
| `ValLatitude` | `[-90, 90]` inclusive |
| `ValLongitude` | `[-180, 180]` inclusive |

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Numeric;
using System.ComponentModel.DataAnnotations;

public class LocationPrice
{
    [ValDecimalPrecision(10, 2)]
    public decimal Price { get; set; }

    [ValMultipleOf(5)]
    public int Quantity { get; set; }

    [ValPercentage]
    public decimal TaxRate { get; set; }

    [ValPercentage(asFraction: true)]
    public double Probability { get; set; }

    [ValLatitude]
    public double Lat { get; set; }

    [ValLongitude]
    public double Lon { get; set; }
}
```

---

## String

**Namespace:** `RzR.Validation.Attributes.Attributes.String`

All string attributes reject null and non-string values. Empty strings are also rejected by `ValAlpha`, `ValAlphaNumeric`, and `ValNumericString`.

**Constructors:**

```csharp
ValLengthRangeAttribute(int min, int max, string userMessage = null)
ValMaxLengthAttribute(int max, string userMessage = null)
ValMinLengthAttribute(int min, string userMessage = null)
ValExactLengthAttribute(int length, string userMessage = null)
ValStartsWithAttribute(string prefix, bool ignoreCase = false, string userMessage = null)
ValEndsWithAttribute(string suffix, bool ignoreCase = false, string userMessage = null)
ValContainsAttribute(string substring, bool ignoreCase = false, string userMessage = null)
ValRegexAttribute(string pattern, string userMessage = null)
ValAlphaAttribute(string userMessage = null)
ValAlphaNumericAttribute(string userMessage = null)
ValNumericStringAttribute(string userMessage = null)
```

Length bounds are inclusive. `ValRegex` applies a 2-second match timeout on net45 and netstandard2.0 to prevent ReDoS; invalid patterns or timeouts fail closed.

`ValAlpha` - only ASCII letters (a–z, A–Z).
`ValAlphaNumeric` - only ASCII letters and digits (a–z, A–Z, 0–9).
`ValNumericString` - only ASCII digits (0–9).

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.String;
using System.ComponentModel.DataAnnotations;

public class ArticleRequest
{
    [ValLengthRange(3, 200)]
    public string Title { get; set; }

    [ValMinLength(10)]
    public string Body { get; set; }

    [ValMaxLength(160)]
    public string Summary { get; set; }

    [ValExactLength(2)]
    public string LanguageCode { get; set; }

    [ValStartsWith("ARTICLE-")]
    public string ReferenceCode { get; set; }

    [ValEndsWith(".md")]
    public string FileName { get; set; }

    [ValContains("@", ignoreCase: false)]
    public string MentionField { get; set; }

    [ValRegex(@"^\d{4}-\d{2}-\d{2}$")]
    public string DateString { get; set; }

    [ValAlpha]
    public string CountryName { get; set; }

    [ValAlphaNumeric]
    public string Token { get; set; }

    [ValNumericString]
    public string PinCode { get; set; }
}
```

---

## Format

**Namespace:** `RzR.Validation.Attributes.Attributes.Format`

**Constructors:**

```csharp
ValIpAddressAttribute(string userMessage = null)
ValHexColorAttribute(string userMessage = null)
ValBase64Attribute(string userMessage = null)
ValPhoneE164Attribute(string userMessage = null)
ValIbanAttribute(string userMessage = null)
ValColorNameAttribute(string userMessage = null)
```

| Attribute | What passes |
| --- | --- |
| `ValIpAddress` | Valid IPv4 or IPv6 address (uses `IPAddress.TryParse` on net45/netstandard2.0; regex on netstandard1.1) |
| `ValHexColor` | `#RGB`, `#RRGGBB`, or `#RRGGBBAA` |
| `ValBase64` | Valid Base64-encoded string with correct padding (length must be a multiple of 4) |
| `ValPhoneE164` | E.164 international format: `+` then a non-zero country digit, then 1–14 digits |
| `ValIban` | Structurally valid IBAN per ISO 13616 mod-97 check; spaces and hyphens are accepted as separators |
| `ValColorName` | A recognised CSS3/X11 named color (case-insensitive, e.g. `"red"`, `"CornflowerBlue"`) |

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Format;
using System.ComponentModel.DataAnnotations;

public class PaymentDetails
{
    [ValIpAddress]
    public string ClientIp { get; set; }

    [ValHexColor]
    public string BrandColor { get; set; }

    [ValBase64]
    public string Payload { get; set; }

    [ValPhoneE164]
    public string Phone { get; set; }

    [ValIban]
    public string BankAccount { get; set; }

    [ValColorName]
    public string ThemeColor { get; set; }
}
```

---

## Identity and contact

**Namespace:** `RzR.Validation.Attributes.Attributes.Identity`

**Constructors:**

```csharp
ValEmailAttribute(string userMessage = null)
ValUrlAttribute(bool requireHttps = false, string userMessage = null)
ValCreditCardAttribute(string userMessage = null)
ValUsernameAttribute(int minLength = 3, int maxLength = 20, string userMessage = null)
ValCountryCodeAttribute(string userMessage = null)
ValCultureCodeAttribute(string userMessage = null)
ValSlugAttribute(string userMessage = null)
ValPostalCodeAttribute(string country = "US", string userMessage = null)
```

Notes:

- `ValEmail` checks structure only (max 254 chars, local part max 64 chars, basic format regex). No DNS or mailbox verification.
- `ValUrl` accepts HTTP and HTTPS absolute URLs by default. Pass `requireHttps: true` to reject plain HTTP.
- `ValCreditCard` validates the Luhn checksum. Spaces and hyphens are stripped before checking. Does not verify the card number against a payment network.
- `ValUsername` requires the string to start with an alphanumeric character; the remainder may contain alphanumeric characters, underscores, or hyphens.
- `ValCountryCode` validates against the full ISO 3166-1 alpha-2 code list (case-insensitive).
- `ValCultureCode` validates BCP-47 structural format (e.g. `"en"`, `"en-US"`, `"zh-Hans"`, `"zh-Hans-CN"`). Does not verify against the IANA language subtag registry.
- `ValSlug` requires lowercase letters and digits separated by single hyphens; must start and end with an alphanumeric character.
- `ValPostalCode` supports country-specific structural patterns for 60+ countries identified by ISO 3166-1 alpha-2 codes. Falls back to a generic alphanumeric pattern for unlisted country codes. Patterns validate structure only - not membership in an official postal registry.

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Identity;
using System.ComponentModel.DataAnnotations;

public class UserProfile
{
    [ValEmail]
    public string Email { get; set; }

    [ValUrl(requireHttps: true)]
    public string Website { get; set; }

    [ValCreditCard]
    public string CardNumber { get; set; }

    [ValUsername(minLength: 4, maxLength: 16)]
    public string Handle { get; set; }

    [ValCountryCode]
    public string Country { get; set; }       // e.g. "US", "gb", "DE"

    [ValCultureCode]
    public string Locale { get; set; }        // e.g. "en-US", "zh-Hans"

    [ValSlug]
    public string ProfileSlug { get; set; }   // e.g. "alice-smith-42"

    [ValPostalCode("US")]
    public string ZipCode { get; set; }

    [ValPostalCode("GB")]
    public string PostCode { get; set; }

    [ValPostalCode("DE")]
    public string GermanPostCode { get; set; }
}
```

---

## Enum and miscellaneous

**Namespace:** `RzR.Validation.Attributes.Attributes.Common`

### ValEnum

Validates that a value is a defined member of a specified enum type.

```csharp
ValEnumAttribute(Type enumType, string propertyName, string userMessage = null)
```

`propertyName` is used in the default error message. If you pass an empty string, the validation framework supplies the property name automatically.

`EnumValidation` is a deprecated `[Obsolete]` back-compatibility alias for `ValEnumAttribute`. Use `ValEnum` in new code.

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Common;
using System.ComponentModel.DataAnnotations;

public enum OrderStatus { Pending, Confirmed, Shipped, Cancelled }

public class UpdateOrderRequest
{
    [ValEnum(typeof(OrderStatus), nameof(Status))]
    public int Status { get; set; }
}
```

### ValAjaxOnly

Validates that a `bool` property is `true`, indicating the caller has flagged the request as an AJAX request.

```csharp
ValAjaxOnlyAttribute(string userMessage = null)
```

This is not a security control. Any client can set the underlying field to `true` regardless of whether the request is actually an AJAX request. Real AJAX enforcement must be performed server-side, typically via an MVC `ActionFilter` that reads the `X-Requested-With: XMLHttpRequest` header. This attribute does not substitute for authentication, authorization, or anti-CSRF tokens.

`AjaxOnly` is a deprecated `[Obsolete]` back-compatibility alias. Use `ValAjaxOnly` in new code.

**Example:**

```csharp
using RzR.Validation.Attributes.Attributes.Common;
using System.ComponentModel.DataAnnotations;

public class AjaxRequest
{
    [ValAjaxOnly]
    public bool IsAjax { get; set; }

    public string Payload { get; set; }
}
```

---

## Custom error messages

Most attributes accept `userMessage` as the last constructor argument:

```csharp
public class OrderRequest
{
    [ValRequiredNotNull("Order reference is required.")]
    public string Reference { get; set; }

    [ValGreaterThan(0, "Amount must be a positive number.")]
    public decimal Amount { get; set; }

    [ValBetween(1, 100, userMessage: "Quantity must be between 1 and 100.")]
    public int Quantity { get; set; }

    [ValEmail("Please supply a valid email address.")]
    public string Email { get; set; }
}
```

For `ValAllowedValues`, `ValDeniedValues`, and the object-level attributes, set `ErrorMessage` via object initializer syntax:

```csharp
[ValAllowedValues("card", "invoice", "wallet") { ErrorMessage = "Payment method is not supported." }]
public string PaymentMethod { get; set; }

[ValAtLeastOneOf("Email", "Phone") { ErrorMessage = "Provide at least one contact method." }]
public class ContactRequest { ... }
```

---

## Reading validation results

After calling `Validator.TryValidateObject`, iterate `validationResults` to collect error messages per property:

```csharp
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

var model = new CreateAccountRequest();
var results = new List<ValidationResult>();
bool isValid = Validator.TryValidateObject(
    model,
    new ValidationContext(model),
    results,
    validateAllProperties: true);

if (!isValid)
{
    foreach (ValidationResult result in results)
    {
        // result.ErrorMessage - the error text
        // result.MemberNames - the property names this error applies to
        foreach (string memberName in result.MemberNames)
            Console.WriteLine($"{memberName}: {result.ErrorMessage}");
    }
}
```

Object-level attribute errors (`ValAtLeastOneOf`, etc.) have an empty `MemberNames` collection because they are not tied to a single property.
