// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValEnumAttribute.cs" company="RzR SOFT & TECH">
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

#if NETSTANDARD1_1
using System.Reflection;
#endif

#endregion

namespace RzR.Validation.Attributes.Attributes.Common
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that a value is a defined member of a specified enum type.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValEnumAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the target enum type to validate against.
        /// </summary>
        /// =================================================================================================
        private readonly Type _enumType;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the property name used in the default error message.
        /// </summary>
        /// =================================================================================================
        private readonly string _propertyName;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) optional caller-supplied override message.
        /// </summary>
        /// =================================================================================================
        private readonly string _customUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValEnumAttribute" /> class.
        /// </summary>
        /// <param name="enumType">The enum type to validate against.</param>
        /// <param name="propertyName">The property name to include in the default error message.</param>
        /// <param name="userMessage">(Optional) Custom error message that overrides the default.</param>
        /// =================================================================================================
        public ValEnumAttribute(Type enumType, string propertyName, string userMessage = null)
            : base(string.Empty)
        {
            _enumType = enumType;
            _propertyName = propertyName;
            _customUserMessage = userMessage;
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
            => string.IsNullOrEmpty(_customUserMessage)
                ? string.Format(Message.DefaultErrorMessage_InvalidEnum, string.IsNullOrEmpty(_propertyName) ? name : _propertyName)
                : _customUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        ///     True if the value is a defined member of <see cref="_enumType" />; false otherwise.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
        {
            if (value == null || _enumType == null
#if NETSTANDARD1_1
                              || !_enumType.GetTypeInfo().IsEnum)
#else
                || !_enumType.IsEnum)
#endif
                return false;

            try
            {
                return Enum.IsDefined(_enumType, value);
            }
            catch
            {
                return false;
            }
        }
    }
}