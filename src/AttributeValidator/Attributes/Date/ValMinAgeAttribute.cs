// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValMinAgeAttribute.cs" company="RzR SOFT & TECH">
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
    ///     Attribute that validates a date of birth represents a person who has reached a minimum
    ///     age.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValMinAgeAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the minimum required age in years.
        /// </summary>
        /// =================================================================================================
        private readonly int _minAgeYears;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) whether to compare against UTC today.
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
        ///     Initializes a new instance of the <see cref="ValMinAgeAttribute" /> class.
        /// </summary>
        /// <param name="minAgeYears">The minimum required age in whole years.</param>
        /// <param name="useUtc">
        ///     (Optional) When true, compares against UTC today; otherwise local today.
        /// </param>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValMinAgeAttribute(int minAgeYears, bool useUtc = true, string userMessage = null) 
            : base(Message.DefaultErrorMessage_MinAge)
        {
            _minAgeYears = minAgeYears;
            _useUtc = useUtc;
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
                ? string.Format(Message.DefaultErrorMessage_MinAge, name, _minAgeYears)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value.
        /// </summary>
        /// <param name="value">The date of birth to validate.</param>
        /// <returns>
        ///     True if the computed age is at least the minimum; false otherwise.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            if (value == null)
                return false;

            if (value is DateTime dob)
            {
                var today = (_useUtc ? DateTime.UtcNow : DateTime.Now).Date;
                var age = today.Year - dob.Year;

                if (dob.Date > today.AddYears(-age))
                    age--;

                return age >= _minAgeYears;
            }

            return false;
        }
    }
}