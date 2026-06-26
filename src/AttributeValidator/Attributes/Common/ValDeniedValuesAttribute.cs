// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValDeniedValuesAttribute.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using RzR.Validation.Attributes.Extensions;
using RzR.Validation.Attributes.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

namespace RzR.Validation.Attributes.Attributes.Common
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for validating that a value is not one of an explicit set of denied values.
    ///     Comparison is performed with <see cref="object.Equals(object,object)" />, so null can be
    ///     explicitly denied when it is present in the set. 
    ///     To supply a custom error message set <see cref="ValidationAttribute.ErrorMessage" />
    ///     after construction or use the object initializer syntax.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValDeniedValuesAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the set of values that are not permitted.
        /// </summary>
        /// =================================================================================================
        private readonly object[] _deniedValues;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValDeniedValuesAttribute" /> class.
        /// </summary>
        /// <param name="deniedValues">The explicit set of values that fail validation.</param>
        /// =================================================================================================
        public ValDeniedValuesAttribute(params object[] deniedValues) : base(string.Empty)
            => _deniedValues = deniedValues;

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
            => string.IsNullOrEmpty(ErrorMessage)
                ? string.Format(Message.DefaultErrorMessage_DeniedValues, name)
                : ErrorMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value. Returns true when <paramref name="value" /> does
        ///     not equal any entry in the denied set. A null or empty denied set means every value is
        ///     allowed.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        /// =================================================================================================
        private bool ValidateObject(object value)
            => _deniedValues == null || !_deniedValues.Any(v => MemberHelper.AreEqual(value, v));
    }
}