// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.AttributeValidator
//  Author           : RzR
//  Created On       : 2024-04-23 17:20
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-24 20:33
// ***********************************************************************
//  <copyright file="ValRequiredNotDefaultAttribute.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AttributeValidator.Resources;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace AttributeValidator.Attributes.Require
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for required not default.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
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
        public override bool IsValid(object value) => value != default;
#endif

#if NETSTANDARD1_1
        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = string.Format(Message.DefaultErrorMessage_NotNull, validationContext.MemberName);

            return value != default ? ValidationResult.Success : new ValidationResult(message);
        }
#endif

        /// <inheritdoc/>
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(CustomUserMessage)
                ? string.Format(Message.DefaultErrorMessage_NotDefault, name)
                : CustomUserMessage;
    }
}