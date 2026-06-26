// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 20:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValBetweenAttribute.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using RzR.Validation.Attributes.Extensions;
using RzR.Validation.Attributes.Resources;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace RzR.Validation.Attributes.Attributes.Range
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for value between a minimum and maximum.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValBetweenAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the minimum bound.
        /// </summary>
        /// =================================================================================================
        private readonly object _min;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the maximum bound.
        /// </summary>
        /// =================================================================================================
        private readonly object _max;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) whether the bounds are inclusive.
        /// </summary>
        /// =================================================================================================
        private readonly bool _inclusive;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValBetweenAttribute" /> class.
        /// </summary>
        /// <param name="min">The minimum bound.</param>
        /// <param name="max">The maximum bound.</param>
        /// <param name="inclusive">(Optional) Whether the comparison is inclusive. Default is true.</param>
        /// <param name="userMessage">(Optional) The user message.</param>
        /// =================================================================================================
        public ValBetweenAttribute(object min, object max, bool inclusive = true, string userMessage = null) : base(string.Empty)
        {
            _min = min;
            _max = max;
            _inclusive = inclusive;
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
                ? string.Format(Message.DefaultErrorMessage_Between, name, _min, _max)
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
            if (!ValueComparer.TryCompare(value, _min, out var lo))
                return false;

            if (!ValueComparer.TryCompare(value, _max, out var hi))
                return false;

            return _inclusive 
                ? lo >= 0 && hi <= 0 
                : lo > 0 && hi < 0;
        }
    }
}