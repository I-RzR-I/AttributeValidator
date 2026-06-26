// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 20:06
// 
//  Last Modified By : RzR
//  Last Modified On : 25-06-2026 23:29
//  ***********************************************************************
//  <copyright file="ValCollectionNotEmptyAttribute.cs" company="RzR SOFT & TECH">
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
using System.Collections;
using System.ComponentModel.DataAnnotations;

#endregion

namespace RzR.Validation.Attributes.Attributes.Collection
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for validating that a collection, enumerable, or string value is not null and
    ///     contains at least one element. For <see cref="ICollection" /> the Count property is used;
    ///     for other <see cref="IEnumerable" /> values the enumerator is advanced once to check.
    /// 
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValCollectionNotEmptyAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) optional caller-supplied override message.
        /// </summary>
        /// =================================================================================================
        private readonly string CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValCollectionNotEmptyAttribute" /> class.
        /// </summary>
        /// <param name="userMessage">(Optional) Custom error message that overrides the default.</param>
        /// =================================================================================================
        public ValCollectionNotEmptyAttribute(string userMessage = null) : base(Message.DefaultErrorMessage_CollectionNotEmpty)
            => CustomUserMessage = userMessage;

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
            => string.IsNullOrEmpty(CustomUserMessage)
                ? string.Format(Message.DefaultErrorMessage_CollectionNotEmpty, name)
                : CustomUserMessage;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the object described by value. Null is always invalid. Strings are checked with
        ///     <see cref="string.IsNullOrEmpty" />.
        ///     <see cref="ICollection" /> uses Count; other <see cref="IEnumerable" /> values use
        ///     MoveNext.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        /// =================================================================================================
        private static bool ValidateObject(object value)
        {
            if (value == null)
                return false;

            if (value is string str)
                return !string.IsNullOrEmpty(str);

            if (value is ICollection col)
                return col.Count > 0;

            if (value is IEnumerable en)
            {
                var enumerator = en.GetEnumerator();
                try
                {
                    return enumerator.MoveNext();
                }
                finally
                {
                    var disposable = enumerator as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                }
            }

            return false;
        }
    }
}