// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValRequiredNotNullAttribute.cs" company="RzR SOFT & TECH">
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

namespace RzR.Validation.Attributes.Attributes.Require
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for required not null.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValRequiredNotNullAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValRequiredNotNullAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValRequiredNotNullAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_NotNull)
            => CustomUserMessage = userMessage;

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc/>
        public override bool IsValid(object value) => value != null;
#endif

#if NETSTANDARD1_1
        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            => value != null
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage(validationContext.MemberName));
#endif

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(CustomUserMessage)
                ? string.Format(Message.DefaultErrorMessage_NotNull, name)
                : CustomUserMessage;
    }
}