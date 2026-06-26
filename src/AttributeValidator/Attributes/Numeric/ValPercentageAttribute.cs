// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValPercentageAttribute.cs" company="RzR SOFT & TECH">
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
    ///     Attribute that validates a numeric value is a valid percentage. When asFraction is false
    ///     (default) the accepted range is 0-100. When asFraction is true the accepted range is 0-1.
    ///     Accepts any numeric type or a numeric string; conversion uses
    ///     <see cref="CultureInfo.InvariantCulture" /> so decimal separators are always dots.
    /// 
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValPercentageAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) when true the valid range is [0, 1] instead of [0, 100].
        /// </summary>
        /// =================================================================================================
        private readonly bool _asFraction;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValPercentageAttribute" /> class.
        /// </summary>
        /// <param name="asFraction">(Optional) When true validates the value is in [0, 1]; when false validates [0, 100].</param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValPercentageAttribute(bool asFraction = false, string userMessage = null) : base(string.Empty)
        {
            _asFraction = asFraction;
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
                ? string.Format(Message.DefaultErrorMessage_Percentage, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value.
        /// </summary>
        /// <param name="value">The numeric value to validate.</param>
        /// <returns>
        ///     True if the value is within the allowed percentage range; false otherwise.
        ///     Returns false for null values or values that cannot be converted to decimal.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            if (value == null)
                return false;

            try
            {
                var decimalValue = Convert.ToDecimal(value, CultureInfo.InvariantCulture);

                return _asFraction 
                    ? decimalValue >= 0m && decimalValue <= 1m
                    : decimalValue >= 0m && decimalValue <= 100m;
            }
            catch
            {
                return false;
            }
        }
    }
}