// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValSlugAttribute.cs" company="RzR SOFT & TECH">
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
    ///     Validates that a string value is a well-formed URL slug. Slugs must be lowercase, contain
    ///     only alphanumeric characters and hyphens, start and end with an alphanumeric character,
    ///     and not contain consecutive hyphens. Examples: "my-slug", "article-123", "hello-world".
    /// 
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValSlugAttribute : ValidationAttribute
    {
        private const string Pattern = @"^[a-z0-9]+(?:-[a-z0-9]+)*$";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValSlugAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValSlugAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_Slug)
            => CustomUserMessage = userMessage;

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
                ? string.Format(Message.DefaultErrorMessage_Slug, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value. Rejects null, empty strings, and strings that do
        ///     not match the lowercase slug pattern.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        /// =================================================================================================
        private static bool ValidateObject(object value)
        {
            try
            {
                var s = value as string;
                if (string.IsNullOrEmpty(s))
                    return false;

#if NETSTANDARD1_1
                return Regex.IsMatch(s, Pattern);
#else
                return Regex.IsMatch(s, Pattern, RegexOptions.None, TimeSpan.FromSeconds(2));
#endif
            }
            catch
            {
                return false;
            }
        }
    }
}