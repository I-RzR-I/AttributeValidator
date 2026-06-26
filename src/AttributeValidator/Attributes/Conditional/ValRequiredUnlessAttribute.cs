// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValRequiredUnlessAttribute.cs" company="RzR SOFT & TECH">
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
    ///     Requires the decorated property to be populated UNLESS another property satisfies the
    ///     given comparison condition. When the condition is met the decorated property is exempt
    ///     from the requirement; when the condition is not met the property must be populated.
    /// 
    /// </summary>
    /// <remarks>
    ///     Requires <see cref="ValidationContext" /> so it always uses the protected
    ///     <see cref="IsValid(object, ValidationContext)" /> override across all target frameworks.
    /// 
    /// </remarks>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValRequiredUnlessAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the value the sibling is compared against.
        /// </summary>
        /// =================================================================================================
        private readonly object _comparand;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the caller-supplied error message; null means use the default.
        /// </summary>
        /// =================================================================================================
        private readonly string _customUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the operator to apply when evaluating the sibling value.
        /// </summary>
        /// =================================================================================================
        private readonly ValOp _op;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the name of the sibling property or field to evaluate.
        /// </summary>
        /// =================================================================================================
        private readonly string _otherProperty;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValRequiredUnlessAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The name of the sibling property or field to evaluate.</param>
        /// <param name="op">The comparison operator to apply to the sibling value.</param>
        /// <param name="comparand">The value the sibling is compared against.</param>
        /// <param name="userMessage">
        ///     (Optional) A caller-supplied error message that overrides the default.
        /// </param>
        /// =================================================================================================
        public ValRequiredUnlessAttribute(string otherProperty, ValOp op, object comparand, string userMessage = null)
            : base(string.Empty)
        {
            _otherProperty = otherProperty;
            _op = op;
            _comparand = comparand;
            _customUserMessage = userMessage;
        }

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var other = MemberHelper.GetMemberValue(validationContext.ObjectInstance, _otherProperty);

            if (!MemberHelper.Evaluate(other, _op, _comparand))
            {
                return MemberHelper.IsPopulated(value)
                    ? ValidationResult.Success
                    : new ValidationResult(FormatErrorMessage(validationContext.MemberName));
            }

            return ValidationResult.Success;
        }

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
            => string.IsNullOrEmpty(_customUserMessage)
                ? string.Format(Message.DefaultErrorMessage_Required, name)
                : _customUserMessage;
    }
}