// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValComparePropertyAttribute.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using RzR.Validation.Attributes.Common;
using RzR.Validation.Attributes.Extensions;
using RzR.Validation.Attributes.Resources;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace RzR.Validation.Attributes.Attributes.Conditional
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates that the decorated property satisfies the given comparison operator relative to
    ///     another property on the same object. When the sibling property cannot be found or the
    ///     values are not comparable (e.g. null or unsupported type), ordering comparisons fail
    ///     closed and return a validation error.
    /// </summary>
    /// <remarks>
    ///     Requires <see cref="ValidationContext" /> so it always uses the protected
    ///     <see cref="IsValid(object, ValidationContext)" /> override across all target frameworks.
    /// 
    /// </remarks>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValComparePropertyAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the caller-supplied error message; null means use the default.
        /// </summary>
        /// =================================================================================================
        private readonly string _customUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the operator to apply between the decorated value and the sibling value.
        /// </summary>
        /// =================================================================================================
        private readonly ValOp _op;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the name of the sibling property or field to compare against.
        /// </summary>
        /// =================================================================================================
        private readonly string _otherProperty;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValComparePropertyAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">
        ///     The name of the sibling property or field to compare against.
        /// </param>
        /// <param name="op">
        ///     The comparison operator to apply between this value and the sibling value.
        /// </param>
        /// <param name="userMessage">
        ///     (Optional) A caller-supplied error message that overrides the default.
        /// </param>
        /// =================================================================================================
        public ValComparePropertyAttribute(string otherProperty, ValOp op, string userMessage = null)
            : base(string.Empty)
        {
            _otherProperty = otherProperty;
            _op = op;
            _customUserMessage = userMessage;
        }

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var other = MemberHelper.GetMemberValue(validationContext.ObjectInstance, _otherProperty);

            return MemberHelper.Evaluate(value, _op, other)
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage(validationContext.MemberName));
        }

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(_customUserMessage)
                ? string.Format(Message.DefaultErrorMessage_CompareProperty, name, _otherProperty)
                : _customUserMessage;
    }
}