// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:30
//  ***********************************************************************
//  <copyright file="ValMutuallyExclusiveAttribute.cs" company="RzR SOFT & TECH">
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

namespace RzR.Validation.Attributes.Attributes.Object
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Class-level attribute that validates at most one of the named properties is populated.
    ///     Setting two or more properties in the group simultaneously is invalid.
    ///     Apply to a class; the attribute inspects the instance via reflection.
    /// </summary>
    /// <remarks>
    ///     This attribute runs only under <c>Validator.TryValidateObject</c> with
    ///     <c>validateAllProperties: true</c>, because class-level attributes are evaluated on the
    ///     object instance rather than on individual properties.
    ///     Multiple independent groups can be declared on the same class by applying the attribute
    ///     more than once (<c>AllowMultiple = true</c>).
    ///     A null object instance is treated as valid; per-property null-checking is not the concern
    ///     of this attribute.
    /// </remarks>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ValMutuallyExclusiveAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the property names that form the mutually exclusive group.
        /// </summary>
        /// =================================================================================================
        private readonly string[] _propertyNames;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValMutuallyExclusiveAttribute" /> class.
        /// </summary>
        /// <param name="propertyNames">
        ///     The names of the properties on the decorated class of which at most one may be populated.
        /// </param>
        /// =================================================================================================
        public ValMutuallyExclusiveAttribute(params string[] propertyNames) : base(string.Empty)
            => _propertyNames = propertyNames ?? new string[0];

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var populatedCount = _propertyNames.Count(p => MemberHelper.IsPopulated(MemberHelper.GetMemberValue(value, p)));
           
            if (populatedCount <= 1)
                return ValidationResult.Success;

            return new ValidationResult(Compose());
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
                ? string.Format(Message.DefaultErrorMessage_MutuallyExclusive, string.Join(", ", _propertyNames))
                : ErrorMessage;
    }
}