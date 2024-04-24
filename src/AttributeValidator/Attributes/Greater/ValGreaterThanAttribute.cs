// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.AttributeValidator
//  Author           : RzR
//  Created On       : 2024-04-24 00:24
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-24 19:45
// ***********************************************************************
//  <copyright file="ValGreaterThanAttribute.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AttributeValidator.Extensions;
using AttributeValidator.Resources;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace AttributeValidator.Attributes.Greater
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for value greater than.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValGreaterThanAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the greater than value.
        /// </summary>
        /// =================================================================================================
        private readonly object GreaterThanValue;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValGreaterThanAttribute" /> class.
        /// </summary>
        /// <param name="greaterThan">The greater than.</param>
        /// =================================================================================================
        public ValGreaterThanAttribute(object greaterThan) : base(string.Empty)
            => GreaterThanValue = greaterThan;

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc />
        public override bool IsValid(object value) => ValidateObject(value);
#endif

#if NETSTANDARD1_1
        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = string.Format(Message.DefaultErrorMessage_NotNull, validationContext.MemberName);

            return ValidateObject(value) ? ValidationResult.Success : new ValidationResult(message);
        }
#endif

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
            => string.Format(Message.DefaultErrorMessage_GreaterThan, name, GreaterThanValue);

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
            if (value == null)
                return false;

            var type = value.GetType().GetNonNullableType();

            if (type.IsNumberNoPointType())
                return Convert.ToInt64(value) > Convert.ToInt64(GreaterThanValue);
            if (type.IsNumberUnSignedNoPointType())
                return Convert.ToUInt64(value) > Convert.ToUInt64(GreaterThanValue);
            if (type.IsNumberWithPointType())
                return Convert.ToDecimal(value) > Convert.ToDecimal(GreaterThanValue);
            if (type.IsDateType())
                return Convert.ToDateTime(value) > Convert.ToDateTime(GreaterThanValue);
            if (type.IsTimeSpanType())
                return TimeSpan.Parse(value.ToString()) > TimeSpan.Parse(GreaterThanValue.ToString());
            return false;
        }
    }
}