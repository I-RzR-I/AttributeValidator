// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValEndsWithAttribute.cs" company="RzR SOFT & TECH">
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

#endregion

namespace RzR.Validation.Attributes.Attributes.String
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute that validates a string ends with the specified suffix.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValEndsWithAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the required suffix.
        /// </summary>
        /// =================================================================================================
        private readonly string _suffix;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) whether to compare in a case-insensitive manner.
        /// </summary>
        /// =================================================================================================
        private readonly bool _ignoreCase;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValEndsWithAttribute" /> class.
        /// </summary>
        /// <param name="suffix">The suffix the value must end with.</param>
        /// <param name="ignoreCase">(Optional) Whether to ignore case when comparing. Default is false.</param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValEndsWithAttribute(string suffix, bool ignoreCase = false, string userMessage = null) : base(string.Empty)
        {
            _suffix = suffix;
            _ignoreCase = ignoreCase;
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
                ? string.Format(Message.DefaultErrorMessage_EndsWith, name, _suffix)
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

            if (s == null || _suffix == null) 
                return false;

            return s.EndsWith(_suffix, _ignoreCase
                ? StringComparison.OrdinalIgnoreCase 
                : StringComparison.Ordinal);
        }
    }
}