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
