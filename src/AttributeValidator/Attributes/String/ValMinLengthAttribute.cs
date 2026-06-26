// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValMinLengthAttribute.cs" company="RzR SOFT & TECH">
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
    ///     Attribute that validates a string's length meets the specified minimum.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValMinLengthAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the minimum allowed length.
        /// </summary>
        /// =================================================================================================
        private readonly int _min;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValMinLengthAttribute" /> class.
        /// </summary>
        /// <param name="min">The minimum allowed string length (inclusive).</param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValMinLengthAttribute(int min, string userMessage = null) : base(string.Empty)
        {
            _min = min;
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
                ? string.Format(Message.DefaultErrorMessage_MinLength, name, _min)
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

            if (s == null)
                return false;

            return s.Length >= _min;
        }
    }
}