// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 26-06-2026 20:23
//  ***********************************************************************
//  <copyright file="Message.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

namespace RzR.Validation.Attributes.Resources
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A message resource.
    /// </summary>
    /// =================================================================================================
    internal static class Message
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not empty.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotEmpty = "The {0} field must not be empty!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not null.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotNull = "The {0} field must not be null!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not default.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotDefault = "The {0} field must not be with default value!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message positive.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Positive = "The {0} field must be greater than 0.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message greater than.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_GreaterThan = "The {0} field must be greater than {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message invalid enum.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_InvalidEnum = "The {0} field has an invalid value.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message less than.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_LessThan = "The {0} field must be less than {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message greater than or equal.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_GreaterThanOrEqual = "The {0} field must be greater than or equal to {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message less than or equal.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_LessThanOrEqual = "The {0} field must be less than or equal to {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message between.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Between = "The {0} field must be between {1} and {2}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not white space.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotWhiteSpace = "The {0} field must not be empty or whitespace.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message guid not empty.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_GuidNotEmpty = "The {0} field must not be an empty GUID.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message collection not empty.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_CollectionNotEmpty = "The {0} field must contain at least one item.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message allowed values.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_AllowedValues = "The {0} field has a value that is not allowed.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message denied values.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_DeniedValues = "The {0} field has a value that is not permitted.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message required.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Required = "The {0} field is required.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message compare property.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_CompareProperty = "The {0} field is not valid in comparison to the {1} field.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message at least one of.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_AtLeastOneOf = "At least one of the following fields is required: {0}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message mutually exclusive.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_MutuallyExclusive = "Only one of the following fields can be set: {0}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message exactly one of.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_ExactlyOneOf = "Exactly one of the following fields must be set: {0}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message chronological.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Chronological = "The following fields must be in chronological order: {0}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not future.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotFuture = "The {0} field must not be a future date.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not past.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotPast = "The {0} field must not be a past date.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message minimum age.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_MinAge = "The {0} field must correspond to a minimum age of {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message decimal precision.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_DecimalPrecision = "The {0} field must have at most {1} digits in total and {2} decimal places.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message multiple of.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_MultipleOf = "The {0} field must be a multiple of {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message length range.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_LengthRange = "The {0} field length must be between {1} and {2} characters.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message starts with.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_StartsWith = "The {0} field must start with '{1}'.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message ends with.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_EndsWith = "The {0} field must end with '{1}'.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message contains.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Contains = "The {0} field must contain '{1}'.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message regex.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Regex = "The {0} field has an invalid format.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message IBAN.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Iban = "The {0} field must be a valid IBAN.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message IP address.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_IpAddress = "The {0} field must be a valid IP address.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message hex color.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_HexColor = "The {0} field must be a valid hex color.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message Base64.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Base64 = "The {0} field must be a valid Base64 string.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message E.164 phone number.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_PhoneE164 = "The {0} field must be a valid E.164 phone number.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message email.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Email = "The {0} field must be a valid email address.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message URL.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Url = "The {0} field must be a valid URL.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message credit card.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_CreditCard = "The {0} field must be a valid credit card number.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message username.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Username = "The {0} field must be a valid username.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message country code.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_CountryCode = "The {0} field must be a valid ISO 3166-1 alpha-2 country code.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message max length.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_MaxLength = "The {0} field must be at most {1} characters.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message min length.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_MinLength = "The {0} field must be at least {1} characters.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message exact length.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_ExactLength = "The {0} field must be exactly {1} characters.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message equal.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Equal = "The {0} field must equal {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not equal.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotEqual = "The {0} field must not equal {1}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message culture code.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_CultureCode = "The {0} field must be a valid culture code.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message postal code.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_PostalCode = "The {0} field must be a valid postal code.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message slug.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Slug = "The {0} field must be a valid slug.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message percentage.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Percentage = "The {0} field must be a valid percentage.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message latitude.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Latitude = "The {0} field must be a valid latitude between -90 and 90.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message longitude.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Longitude = "The {0} field must be a valid longitude between -180 and 180.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message color name.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_ColorName = "The {0} field must be a valid color name.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message alpha.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Alpha = "The {0} field must contain only letters.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message alpha numeric.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_AlphaNumeric = "The {0} field must contain only letters and digits.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message numeric string.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NumericString = "The {0} field must contain only digits.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message AJAX only.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_AjaxOnly = "AJAX requests only.";
    }
}