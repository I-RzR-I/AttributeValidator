// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValRequiredNotDefaultAttribute.cs" company="RzR SOFT & TECH">
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

namespace RzR.Validation.Attributes.Attributes.Require
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for required not default.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValRequiredNotDefaultAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValRequiredNotDefaultAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValRequiredNotDefaultAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_NotDefault)
            => CustomUserMessage = userMessage;

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc/>
        public override bool IsValid(object value) => IsNotDefault(value);
#endif

#if NETSTANDARD1_1
        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            => IsNotDefault(value) 
                ? ValidationResult.Success 
                : new ValidationResult(FormatErrorMessage(validationContext.MemberName));
#endif

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Returns true when <paramref name="value" /> is not the default for its runtime type.
        ///     For reference types the default is null; for value types it is the zero-initialized instance.
        /// </summary>
        /// <param name="value">The boxed value to test.</param>
        /// <returns>False when value is null or equals Activator.CreateInstance of its type.</returns>
        /// =================================================================================================
        private static bool IsNotDefault(object value)
        {
            if (value == null)
                return false;
            var type = value.GetType();

#if NETSTANDARD1_1
            if (type.GetTypeInfo().IsValueType)
#else
            if (type.IsValueType)
#endif
                return !value.Equals(Activator.CreateInstance(type));
            return true;
        }

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(CustomUserMessage)
                ? string.Format(Message.DefaultErrorMessage_NotDefault, name)
                : CustomUserMessage;
    }
}