// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 22:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValPostalCodeAttribute.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using RzR.Validation.Attributes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

#endregion

namespace RzR.Validation.Attributes.Attributes.Identity
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that a string value is a well-formed postal code for the specified country.
    ///     Supports country-specific structural patterns for 60+ countries identified by ISO 3166-1
    ///     alpha-2 codes, matched case-insensitively. Patterns validate the structural format of the
    ///     postal code; they do not verify membership in an official postal registry. Falls back to
    ///     a generic alphanumeric pattern for unlisted country codes.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValPostalCodeAttribute : ValidationAttribute
    {
        private const string GenericPattern = @"^[A-Za-z0-9][A-Za-z0-9 -]{1,10}[A-Za-z0-9]$";

        private static readonly Dictionary<string, string> Patterns = new()
        {
            { "US", @"^\d{5}(-\d{4})?$" },
            { "CA", @"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$" },
            { "GB", @"^[A-Za-z]{1,2}\d[A-Za-z\d]? ?\d[A-Za-z]{2}$" },
            { "IE", @"^[A-Za-z]\d{2} ?[A-Za-z0-9]{4}$" },
            { "DE", @"^\d{5}$" },
            { "FR", @"^\d{5}$" },
            { "IT", @"^\d{5}$" },
            { "ES", @"^\d{5}$" },
            { "PT", @"^\d{4}-\d{3}$" },
            { "NL", @"^\d{4} ?[A-Za-z]{2}$" },
            { "BE", @"^\d{4}$" },
            { "LU", @"^\d{4}$" },
            { "AT", @"^\d{4}$" },
            { "CH", @"^\d{4}$" },
            { "LI", @"^\d{4}$" },
            { "DK", @"^\d{4}$" },
            { "NO", @"^\d{4}$" },
            { "SE", @"^\d{3} ?\d{2}$" },
            { "FI", @"^\d{5}$" },
            { "IS", @"^\d{3}$" },
            { "PL", @"^\d{2}-\d{3}$" },
            { "CZ", @"^\d{3} ?\d{2}$" },
            { "SK", @"^\d{3} ?\d{2}$" },
            { "HU", @"^\d{4}$" },
            { "RO", @"^\d{6}$" },
            { "BG", @"^\d{4}$" },
            { "GR", @"^\d{3} ?\d{2}$" },
            { "HR", @"^\d{5}$" },
            { "SI", @"^\d{4}$" },
            { "RS", @"^\d{5,6}$" },
            { "LT", @"^(LT-)?\d{5}$" },
            { "LV", @"^(LV-)?\d{4}$" },
            { "EE", @"^\d{5}$" },
            { "UA", @"^\d{5}$" },
            { "RU", @"^\d{6}$" },
            { "BY", @"^\d{6}$" },
            { "MC", @"^980\d{2}$" },
            { "MD", @"^(MD-?)?\d{4}$" },
            { "AU", @"^\d{4}$" },
            { "NZ", @"^\d{4}$" },
            { "JP", @"^\d{3}-?\d{4}$" },
            { "KR", @"^\d{5}$" },
            { "CN", @"^\d{6}$" },
            { "IN", @"^\d{3} ?\d{3}$" },
            { "SG", @"^\d{6}$" },
            { "MY", @"^\d{5}$" },
            { "TH", @"^\d{5}$" },
            { "ID", @"^\d{5}$" },
            { "PH", @"^\d{4}$" },
            { "VN", @"^\d{6}$" },
            { "PK", @"^\d{5}$" },
            { "BD", @"^\d{4}$" },
            { "IL", @"^\d{5}(\d{2})?$" },
            { "SA", @"^\d{5}(-\d{4})?$" },
            { "EG", @"^\d{5}$" },
            { "ZA", @"^\d{4}$" },
            { "NG", @"^\d{6}$" },
            { "BR", @"^\d{5}-?\d{3}$" },
            { "MX", @"^\d{5}$" },
            { "AR", @"^[A-Za-z]?\d{4}[A-Za-z]{0,3}$" },
            { "CL", @"^\d{7}$" },
            { "CO", @"^\d{6}$" },
            { "PE", @"^\d{5}$" }
        };

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the country code used to select the postal pattern.
        /// </summary>
        /// =================================================================================================
        private readonly string _country;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValPostalCodeAttribute" /> class.
        /// </summary>
        /// <param name="country">(Optional) ISO 3166-1 alpha-2 country code. Defaults to "US".</param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValPostalCodeAttribute(string country = "US", string userMessage = null)
            : base(Message.DefaultErrorMessage_PostalCode)
        {
            _country = country;
            CustomUserMessage = userMessage;
        }

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc/>
        public override bool IsValid(object value)
            => ValidateObject(value);
#endif

#if NETSTANDARD1_1
        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            => ValidateObject(value) 
            ? ValidationResult.Success 
            : new ValidationResult(FormatErrorMessage(validationContext.MemberName));
#endif

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(CustomUserMessage)
                ? string.Format(Message.DefaultErrorMessage_PostalCode, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value. Rejects null, whitespace, and strings that do
        ///     not match the country-specific postal code pattern. Falls back to a generic pattern for
        ///     unrecognised country codes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            try
            {
                var s = value as string;
                if (string.IsNullOrWhiteSpace(s))
                    return false;

                var key = (_country ?? "US").ToUpperInvariant();
                var pattern = Patterns.TryGetValue(key, out var p) ? p : GenericPattern;

#if NETSTANDARD1_1
                return Regex.IsMatch(s, pattern);
#else
                return Regex.IsMatch(s, pattern, RegexOptions.None, TimeSpan.FromSeconds(2));
#endif
            }
            catch
            {
                return false;
            }
        }
    }
}