// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValNotPastAttribute.cs" company="RzR SOFT & TECH">
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

namespace RzR.Validation.Attributes.Attributes.Date
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute that validates a date/time value is not in the past (i.e. &gt;= now).
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValNotPastAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) whether to compare against UTC time.
        /// </summary>
        /// =================================================================================================
        private readonly bool _useUtc;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValNotPastAttribute" /> class.
        /// </summary>
        /// <param name="useUtc">
        ///     (Optional) When true, compares against UTC now; otherwise local now.
        /// </param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValNotPastAttribute(bool useUtc = true, string userMessage = null) 
            : base(Message.DefaultErrorMessage_NotPast)
        {
            _useUtc = useUtc;
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
                ? string.Format(Message.DefaultErrorMessage_NotPast, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        ///     True if the date is not in the past; false otherwise.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            if (value == null)
                return false;

            if (value is DateTime dt)
            {
                var now = _useUtc ? DateTime.UtcNow : DateTime.Now;

                return dt >= now;
            }

            if (value is DateTimeOffset dto)
            {
                var now = _useUtc ? DateTimeOffset.UtcNow : DateTimeOffset.Now;

                return dto >= now;
            }

            return false;
        }
    }
}