// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValIbanAttribute.cs" company="RzR SOFT & TECH">
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
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace RzR.Validation.Attributes.Attributes.Format
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that a string value is a structurally valid IBAN using the ISO 13616 mod-97
    ///     check. Accepts spaces and hyphens as separators; normalises to upper-case before
    ///     validation.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValIbanAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValIbanAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValIbanAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_Iban)
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
                ? string.Format(Message.DefaultErrorMessage_Iban, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value. Implements structural IBAN validation per ISO
        ///     13616: strips separators, checks length bounds, verifies character layout, then performs
        ///     the mod-97 check by converting letters to their numeric equivalents (A=10..Z=35) and
        ///     computing the remainder via chunked integer arithmetic to avoid overflow.
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
                var raw = value as string;

                if (string.IsNullOrWhiteSpace(raw))
                    return false;

                var inputIban = raw.Replace(" ", string.Empty).Replace("-", string.Empty).ToUpperInvariant();

                if (inputIban.Length < 15 || inputIban.Length > 34)
                    return false;

                if (!Regex.IsMatch(inputIban, "^[A-Z]{2}[0-9]{2}[A-Z0-9]+$"))
                    return false;

                var rearranged = inputIban.Substring(4) + inputIban.Substring(0, 4);

                var sb = new StringBuilder();
                foreach (var ch in rearranged)
                {
                    switch (ch)
                    {
                        case >= '0' and <= '9':
                            sb.Append(ch);
                            break;
                        case >= 'A' and <= 'Z':
                            sb.Append((ch - 'A' + 10).ToString());
                            break;
                        default:
                            return false;
                    }
                }

                var digits = sb.ToString();
                var remainder = 0;

                for (var i = 0; i < digits.Length; i++)
                    remainder = ((remainder * 10) + (digits[i] - '0')) % 97;

                return remainder == 1;
            }
            catch
            {
                return false;
            }
        }
    }
}