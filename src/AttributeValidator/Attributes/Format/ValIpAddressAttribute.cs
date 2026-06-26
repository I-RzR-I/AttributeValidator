// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValIpAddressAttribute.cs" company="RzR SOFT & TECH">
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
using System.Net;

#if NETSTANDARD1_1
using System.Text.RegularExpressions;
#endif

#endregion

namespace RzR.Validation.Attributes.Attributes.Format
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that a string value is a valid IP address (IPv4 or IPv6).
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValIpAddressAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) userMessage.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValIpAddressAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Message describing the user.</param>
        /// =================================================================================================
        public ValIpAddressAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_IpAddress)
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
                ? string.Format(Message.DefaultErrorMessage_IpAddress, name)
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
        private static bool ValidateObject(object value)
        {
            var s = value as string;

            if (string.IsNullOrWhiteSpace(s))
                return false;

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
            return IPAddress.TryParse(s, out _);
#elif NETSTANDARD1_1
            try
            {
                // IPv4: four octets 0-255 separated by dots.
                // IPv6: groups of hex digits separated by colons (covers compressed :: notation).
                const string ipv4Pattern = @"^((25[0-5]|2[0-4]\d|1\d{2}|[1-9]\d|\d)\.){3}(25[0-5]|2[0-4]\d|1\d{2}|[1-9]\d|\d)$";
                const string ipv6Pattern = @"^([0-9a-fA-F]{0,4}:){2,7}[0-9a-fA-F]{0,4}$";

                return Regex.IsMatch(s, ipv4Pattern) || Regex.IsMatch(s, ipv6Pattern);
            }
            catch
            {
                return false;
            }
#else
            return false;
#endif
        }
    }
}