// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValCreditCardAttribute.cs" company="RzR SOFT & TECH">
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

namespace RzR.Validation.Attributes.Attributes.Identity
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that a string value is a structurally valid credit card number using the Luhn
    ///     algorithm. Accepts spaces and hyphens as formatting separators; strips them before
    ///     validation. Does not verify the card against a payment network.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValCreditCardAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValCreditCardAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValCreditCardAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_CreditCard)
            => CustomUserMessage = userMessage;

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
                ? string.Format(Message.DefaultErrorMessage_CreditCard, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value. Strips spaces and hyphens, checks that the
        ///     result contains only digits with a length between 12 and 19, then verifies the Luhn
        ///     checksum by iterating from right to left, doubling every second digit and subtracting 9
        ///     when the doubled value exceeds 9.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        /// =================================================================================================
        private static bool ValidateObject(object value)
        {
            try
            {
                var s = value as string;

                if (string.IsNullOrWhiteSpace(s))
                    return false;

                var digits = s.Replace(" ", string.Empty).Replace("-", string.Empty);

                if (digits.Length < 12 || digits.Length > 19)
                    return false;

                foreach (var ch in digits)
                {
                    if (ch < '0' || ch > '9')
                        return false;
                }

                var sum = 0;
                var doubling = false;
                for (var i = digits.Length - 1; i >= 0; i--)
                {
                    var d = digits[i] - '0';
                    if (doubling)
                    {
                        d *= 2;
                        if (d > 9)
                            d -= 9;
                    }

                    sum += d;
                    doubling = !doubling;
                }

                return sum % 10 == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}