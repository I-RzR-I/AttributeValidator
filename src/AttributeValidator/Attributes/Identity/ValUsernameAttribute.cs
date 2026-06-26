// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValUsernameAttribute.cs" company="RzR SOFT & TECH">
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

namespace RzR.Validation.Attributes.Attributes.Identity
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that a string value is an acceptable username: starts with an alphanumeric
    ///     character and the remainder may contain alphanumeric characters, underscores, or hyphens.
    ///     Length is enforced by configurable min/max bounds (default 3–20).
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValUsernameAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the minimum allowed username length.
        /// </summary>
        /// =================================================================================================
        private readonly int _minLength;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the maximum allowed username length.
        /// </summary>
        /// =================================================================================================
        private readonly int _maxLength;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValUsernameAttribute" /> class.
        /// </summary>
        /// <param name="minLength">(Optional) The minimum allowed username length. Default is 3.</param>
        /// <param name="maxLength">
        ///     (Optional) The maximum allowed username length. Default is 20.
        /// </param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValUsernameAttribute(int minLength = 3, int maxLength = 20, string userMessage = null)
            : base(Message.DefaultErrorMessage_Username)
        {
            _minLength = minLength;
            _maxLength = maxLength;
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
                ? string.Format(Message.DefaultErrorMessage_Username, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value. Rejects null, strings outside the configured
        ///     length bounds, and strings that do not start with an alphanumeric character or that
        ///     contain characters other than alphanumerics, underscores, or hyphens.
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

                if (s == null)
                    return false;

                if (s.Length < _minLength || s.Length > _maxLength)
                    return false;

#if NETSTANDARD1_1
                return Regex.IsMatch(s, @"^[A-Za-z0-9][A-Za-z0-9_-]*$");
#else
                return Regex.IsMatch(s, @"^[A-Za-z0-9][A-Za-z0-9_-]*$", RegexOptions.None, TimeSpan.FromSeconds(2));
#endif
            }
            catch
            {
                return false;
            }
        }
    }
}