// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValRegexAttribute.cs" company="RzR SOFT & TECH">
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
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

#endregion

namespace RzR.Validation.Attributes.Attributes.String
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute that validates a string matches the specified regular expression pattern.
    ///     A match timeout of 2 seconds is applied on supported targets to prevent ReDoS attacks.
    ///     An invalid pattern or a timeout causes the validation to return false (fail-closed).
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValRegexAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the regular expression pattern.
        /// </summary>
        /// =================================================================================================
        private readonly string _pattern;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValRegexAttribute" /> class.
        /// </summary>
        /// <param name="pattern">The regular expression pattern the value must match.</param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValRegexAttribute(string pattern, string userMessage = null) : base(string.Empty)
        {
            _pattern = pattern;
            CustomUserMessage = userMessage;
        }

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc/>
        public override bool IsValid(object value) => ValidateObject(value);
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
                ? string.Format(Message.DefaultErrorMessage_Regex, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            var s = value as string;

            if (s == null || _pattern == null) 
                return false;

            try
            {
#if NETSTANDARD1_1
                return Regex.IsMatch(s, _pattern);
#else
                return Regex.IsMatch(s, _pattern, RegexOptions.None, TimeSpan.FromSeconds(2));
#endif
            }
            catch
            {
                return false;
            }
        }
    }
}