### **v3.0.0.6710** [[RzR](mailto:108324929+I-RzR-I@users.noreply.github.com)] 18-07-2026
* [DEV] - (RzR) -> New companion package `RzR.Validation.Attributes.AspNetCore` (net8.0) for ASP.NET Core auto-validation.
* [DEV] - (RzR) -> AspNetCore: Minimal API endpoint filter `ValidationFilter<T>` with `WithValidation<T>()` extensions for `RouteHandlerBuilder` and `RouteGroupBuilder`.
* [DEV] - (RzR) -> AspNetCore: MVC action filter `RzRValidateModelFilter` and the [ValidateModel] attribute.
* [DEV] - (RzR) -> AspNetCore: `AddRzRValidation()` service registration and `RzRValidationOptions` (status code, problem title/type, member-name transform).
* [DEV] - (RzR) -> AspNetCore: RFC 7807 application/problem+json responses on validation failure.
* [DEV] - (RzR) -> New companion package `RzR.Validation.Attributes.Mvvm `(netstandard2.0) with `ValidatableObservableObject` (`INotifyPropertyChanged` + `INotifyDataErrorInfo`); works with `WPF`, `WinUI`, `.NET MAUI`, and `Avalonia`, no WPF dependency.
* [DEV] - (RzR) -> Added ValidationExtensions (RzR.Validation.Attributes.Extensions): `TryValidate(out results)`, `Validate()`, `IsValid()`, and `ValidateAndThrow()` over the standard `Validator` pipeline (validateAllProperties: true); available on net45 and netstandard2.0.
* [DEV] - (RzR) -> Consolidated the internal MemberHelper, ValueComparer, and TypeExtensions helpers under the RzR.Validation.Attributes namespace (no public API impact).
* [DEV] - (RzR) -> Companion packages ship relevant PackageTags, PackageDescription, and Summary.
* [DEV] - (RzR) -> Rewrote README and docs/usage.md; added docs/usage-aspnetcore.md and docs/usage-mvvm.md and linked them from the README.
* [FIX] - (RzR) -> Value-aware equality: `ValEqual`, `ValNotEqual`, `ValAllowedValues`, `ValDeniedValues`, and the conditional attributes (`ValRequiredIf`, `ValRequiredUnless`, `ValCompareProperty`) now compare numeric values across types by value, so an int of 1 matches a comparand of 1L instead of failing on boxed-type mismatch.

### **v2.0.0.106** [[RzR](mailto:108324929+I-RzR-I@users.noreply.github.com)] 27-06-2026
* [DEV] - (RzR) -> Comparison: `ValGreaterThanOrEqual`, `ValLessThan`, `ValLessThanOrEqual`,  `ValBetween`, `ValEqual`, `ValNotEqual`.
* [DEV] - (RzR) -> Presence & sets: `ValNotWhiteSpace`, `ValGuidNotEmpty`, `ValCollectionNotEmpty`,  `ValAllowedValues`, `ValDeniedValues`.
* [DEV] - (RzR) -> Conditional & cross-property: `ValRequiredIf`, `ValRequiredUnless`,  `ValCompareProperty` (use the `ValOp` operator enum).
* [DEV] - (RzR) -> Object-level (class-targeted): `ValAtLeastOneOf`, `ValMutuallyExclusive`,  `ValExactlyOneOf`, `ValChronological`.
* [DEV] - (RzR) -> Date & time: `ValNotFuture`, `ValNotPast`, `ValMinAge`.- Numeric: `ValDecimalPrecision`, `ValMultipleOf`, `ValPercentage`,  `ValLatitude`, `ValLongitude`.
* [DEV] - (RzR) -> String: `ValLengthRange`, `ValMaxLength`, `ValMinLength`, `ValExactLength`,  `ValStartsWith`, `ValEndsWith`, `ValContains`, `ValRegex`, `ValAlpha`,  `ValAlphaNumeric`, `ValNumericString`.
* [DEV] - (RzR) -> Format: `ValIpAddress`, `ValHexColor`, `ValBase64`, `ValPhoneE164`, `ValIban`,  `ValColorName`.
* [DEV] - (RzR) -> Identity & contact: `ValEmail`, `ValUrl`, `ValCreditCard`, `ValUsername`,  `ValCountryCode`, `ValCultureCode`, `ValSlug`, `ValPostalCode` (63 countries with a generic fallback).
* [DEV] - (RzR) -> Internal `ValueComparer` and `MemberHelper` helpers and the public `ValOp` enum.

## v1.0.1.5145
-> Add user custom message on validation.
