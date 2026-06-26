// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValMultipleOfAttribute.cs" company="RzR SOFT & TECH">
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
using System.Globalization;

#endregion

namespace RzR.Validation.Attributes.Attributes.Numeric
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute that validates a numeric value is an exact multiple of a given factor.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValMultipleOfAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the factor the value must be a multiple of.
        /// </summary>
        /// =================================================================================================
        private readonly object _factor;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValMultipleOfAttribute" /> class.
        /// </summary>
        /// <param name="factor">The factor the value must be a multiple of.</param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValMultipleOfAttribute(object factor, string userMessage = null) 
            : base(Message.DefaultErrorMessage_MultipleOf)
        {
            _factor = factor;
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
                ? string.Format(Message.DefaultErrorMessage_MultipleOf, name, _factor)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value.
        /// </summary>
        /// <param name="value">The numeric value to validate.</param>
        /// <returns>
        ///     True if the value is an exact multiple of the factor; false otherwise.
        ///     Returns false when factor is zero to avoid division-by-zero.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            if (value == null)
                return false;

            try
            {
                var inv = CultureInfo.InvariantCulture;
                var v = Convert.ToDecimal(value, inv);
                var f = Convert.ToDecimal(_factor, inv);

                if (f == 0)
                    return false;

                return v % f == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}