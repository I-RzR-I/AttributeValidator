// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValDecimalPrecisionAttribute.cs" company="RzR SOFT & TECH">
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
    ///     Attribute that validates a numeric value fits within the given total digit precision and
    ///     fractional scale (analogous to SQL DECIMAL(precision, scale)).
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValDecimalPrecisionAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) maximum total significant digits allowed.
        /// </summary>
        /// =================================================================================================
        private readonly int _precision;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) maximum decimal places allowed.
        /// </summary>
        /// =================================================================================================
        private readonly int _scale;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValDecimalPrecisionAttribute" /> class.
        /// </summary>
        /// <param name="precision">Maximum total significant digits.</param>
        /// <param name="scale">Maximum decimal places.</param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValDecimalPrecisionAttribute(int precision, int scale, string userMessage = null) 
            : base(Message.DefaultErrorMessage_DecimalPrecision)
        {
            _precision = precision;
            _scale = scale;
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
                ? string.Format(Message.DefaultErrorMessage_DecimalPrecision, name, _precision, _scale)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value.
        /// </summary>
        /// <param name="value">The numeric value to validate.</param>
        /// <returns>
        ///     True if the value fits within the declared precision and scale; false otherwise.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            if (value == null)
                return false;

            try
            {
                var decimalValue = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
                decimalValue = Math.Abs(decimalValue);

                var bits = decimal.GetBits(decimalValue);
                var sc = (bits[3] >> 16) & 0x7F;
                var s = decimalValue.ToString(CultureInfo.InvariantCulture);
                var digits = s.Replace(".", string.Empty).TrimStart('0');
                var totalDigits = digits.Length == 0 ? 1 : digits.Length;

                return sc <= _scale && totalDigits <= _precision;
            }
            catch
            {
                return false;
            }
        }
    }
}