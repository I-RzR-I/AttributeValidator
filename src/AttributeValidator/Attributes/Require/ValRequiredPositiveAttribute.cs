// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.AttributeValidator
//  Author           : RzR
//  Created On       : 2024-04-23 17:58
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-24 20:33
// ***********************************************************************
//  <copyright file="ValRequiredPositiveAttribute.cs" company="">
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

namespace AttributeValidator.Attributes.Require
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for value required positive.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValRequiredPositiveAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValRequiredPositiveAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValRequiredPositiveAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_Positive)
            => CustomUserMessage = userMessage;

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(CustomUserMessage)
                ? string.Format(Message.DefaultErrorMessage_Positive, name)
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
        private static bool ValidateObject(object value)
        {
            if (value == null)
                return false;

            var type = value.GetType().GetNonNullableType();

            if (type.IsNumberNoPointType())
                return Convert.ToInt64(value) > 0;
            if (type.IsNumberUnSignedNoPointType())
                return Convert.ToUInt64(value) > 0;
            if (type.IsNumberWithPointType())
                return Convert.ToDecimal(value) > 0;
            return false;
        }
    }
}