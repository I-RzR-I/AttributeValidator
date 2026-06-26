// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValChronologicalAttribute.cs" company="RzR SOFT & TECH">
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace RzR.Validation.Attributes.Attributes.Object
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Class-level attribute that validates the named properties form a non-decreasing sequence
    ///     (each value is less than or equal to the next). Null property values are skipped; only
    ///     consecutive non-null values are compared. Designed primarily for <see cref="DateTime" />
    ///     and nullable <see cref="DateTime" /> properties, but works for any type supported by
    ///     <see cref="ValueComparer" />.
    ///     Apply to a class; the attribute inspects the instance via reflection.
    /// </summary>
    /// <remarks>
    ///     This attribute runs only under <c>Validator.TryValidateObject</c> with
    ///     <c>validateAllProperties: true</c>, because class-level attributes are evaluated on the
    ///     object instance rather than on individual properties. Multiple independent sequences can
    ///     be declared on the same class by applying the attribute more than once (<c>AllowMultiple
    ///     = true</c>). A null object instance is treated as valid; per-property null-checking is
    ///     not the concern of this attribute. If <see cref="ValueComparer" /> cannot compare a
    ///     consecutive pair (unsupported type or conversion failure), the sequence is considered
    ///     invalid (fail-closed).
    /// </remarks>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ValChronologicalAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the ordered property names that must form a non-decreasing sequence.
        /// </summary>
        /// =================================================================================================
        private readonly string[] _propertyNames;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValChronologicalAttribute" /> class.
        /// </summary>
        /// <param name="propertyNames">
        ///     The names of the properties on the decorated class, in the order they must appear
        ///     chronologically (non-decreasing). Null values are skipped during comparison.
        /// </param>
        /// =================================================================================================
        public ValChronologicalAttribute(params string[] propertyNames) : base(string.Empty)
            => _propertyNames = propertyNames ?? new string[0];

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var nonNullValues = new List<object>();

            foreach (var name in _propertyNames)
            {
                var memberValue = MemberHelper.GetMemberValue(value, name);
                if (memberValue != null)
                    nonNullValues.Add(memberValue);
            }

            for (var i = 0; i < nonNullValues.Count - 1; i++)
            {
                var prev = nonNullValues[i];
                var next = nonNullValues[i + 1];

                if (!ValueComparer.TryCompare(prev, next, out var cmp) || cmp > 0)
                    return new ValidationResult(Compose());
            }

            return ValidationResult.Success;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Returns the error message: the caller-supplied <see cref="ValidationAttribute.ErrorMessage" />
        ///     when set, otherwise the formatted default.
        /// </summary>
        /// <returns>
        ///     The error message string.
        /// </returns>
        /// =================================================================================================
        private string Compose()
            => string.IsNullOrEmpty(ErrorMessage)
                ? string.Format(Message.DefaultErrorMessage_Chronological, string.Join(", ", _propertyNames))
                : ErrorMessage;
    }
}