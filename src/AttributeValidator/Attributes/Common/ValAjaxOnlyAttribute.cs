// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValAjaxOnlyAttribute.cs" company="RzR SOFT & TECH">
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

namespace RzR.Validation.Attributes.Attributes.Common
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that a boolean property indicates the request was made via AJAX.
    /// </summary>
    /// <remarks>
    ///     This attribute validates a caller-supplied <see langword="bool" /> flag only. It is NOT a
    ///     security control: any client can set the underlying field to <see langword="true" />
    ///     regardless of whether the request is actually an AJAX request. Real AJAX enforcement must
    ///     be performed server-side, typically via an MVC ActionFilter that reads the <c>X-Requested-
    ///     With: XMLHttpRequest</c> header. This attribute is not a substitute for anti-CSRF tokens,
    ///     authentication, or authorization checks.
    /// </remarks>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValAjaxOnlyAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) optional caller-supplied override message.
        /// </summary>
        /// =================================================================================================
        private readonly string _customUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValAjaxOnlyAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Custom error message that overrides the default.</param>
        /// =================================================================================================
        public ValAjaxOnlyAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_AjaxOnly)
            => _customUserMessage = userMessage;

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is bool isAjax && isAjax)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(context?.MemberName));
        }

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(_customUserMessage)
                ? Message.DefaultErrorMessage_AjaxOnly
                : _customUserMessage;
    }
}