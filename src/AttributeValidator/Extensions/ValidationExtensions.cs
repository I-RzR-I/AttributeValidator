// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 27-06-2026 00:06
// 
//  Last Modified By : RzR
//  Last Modified On : 27-06-2026 01:51
//  ***********************************************************************
//  <copyright file="ValidationExtensions.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER

#region U S I N G

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace RzR.Validation.Attributes.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Ergonomic extension helpers that run the full DataAnnotations validation pipeline against
    ///     any object instance.
    /// </summary>
    /// <remarks>
    ///     All methods pass <c>validateAllProperties: true</c> to
    ///     <see cref="Validator.TryValidateObject(object, ValidationContext, System.Collections.Generic.ICollection{System.ComponentModel.DataAnnotations.ValidationResult}, bool)" />
    ///     so that conditional,cross-property, and class-level attributes are executed in 
    ///     addition to single-property attributes.
    /// </remarks>
    /// =================================================================================================
    public static class ValidationExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Attempts to validate the instance and returns whether it is valid, along with all
        ///     validation results.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="instance" /> is null.
        /// </exception>
        /// <param name="instance">The object to validate.</param>
        /// <param name="results">
        ///     [out] When this method returns, contains all <see cref="ValidationResult" /> failures.
        ///     Empty when the object is valid.
        /// </param>
        /// <param name="serviceProvider">
        ///     (Optional) An <see cref="IServiceProvider" /> passed to the
        ///     <see cref="ValidationContext" />. May be null.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the instance is valid; otherwise <c>false</c>.
        /// </returns>
        /// =================================================================================================
        public static bool TryValidate(this object instance, out ICollection<ValidationResult> results,
            IServiceProvider serviceProvider = null)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var context = new ValidationContext(instance, serviceProvider, null);
            var resultList = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(instance, context, resultList, true);
            results = resultList;

            return ok;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the instance and returns all validation failures.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="instance" /> is null.
        /// </exception>
        /// <param name="instance">The object to validate.</param>
        /// <param name="serviceProvider">
        ///     (Optional) An <see cref="IServiceProvider" /> passed to the
        ///     <see cref="ValidationContext" />. May be null.
        /// </param>
        /// <returns>
        ///     An <see cref="ICollection{T}" /> of <see cref="ValidationResult" /> failures. Empty when
        ///     the instance is valid.
        /// </returns>
        /// =================================================================================================
        public static ICollection<ValidationResult> Validate(this object instance, 
            IServiceProvider serviceProvider = null)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var context = new ValidationContext(instance, serviceProvider, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(instance, context, results, true);

            return results;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Returns whether the instance passes all validation rules.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="instance" /> is null.
        /// </exception>
        /// <param name="instance">The object to validate.</param>
        /// <param name="serviceProvider">
        ///     (Optional) An <see cref="IServiceProvider" /> passed to the
        ///     <see cref="ValidationContext" />. May be null.
        /// </param>
        /// <returns>
        ///     <c>true</c> when the instance has no validation errors; otherwise <c>false</c>.
        /// </returns>
        /// =================================================================================================
        public static bool IsValid(this object instance, IServiceProvider serviceProvider = null)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var context = new ValidationContext(instance, serviceProvider, null);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(instance, context, results, true);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Validates the instance and throws a <see cref="ValidationException" /> for the first
        ///     failure encountered.
        /// </summary>
        /// <remarks>
        ///     Delegates directly to <see cref="Validator.ValidateObject(object, ValidationContext, bool)" />
        ///     which throws a
        ///     <see cref="ValidationException" /> on the first failing attribute. This is the
        ///     idiomatic approach recommended by the DataAnnotations runtime — it avoids manually
        ///     collecting and re-throwing results and preserves the original exception context
        ///     (including the failed <see cref="ValidationAttribute" /> reference).
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="instance" /> is null.
        /// </exception>
        /// <exception cref="ValidationException">
        ///     Thrown when the instance fails one or more validation rules. The exception message
        ///     describes the first failure encountered.
        /// </exception>
        /// <param name="instance">The object to validate.</param>
        /// <param name="serviceProvider">
        ///     (Optional) An <see cref="IServiceProvider" /> passed to the
        ///     <see cref="ValidationContext" />. May be null.
        /// </param>
        /// =================================================================================================
        public static void ValidateAndThrow(this object instance, IServiceProvider serviceProvider = null)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var context = new ValidationContext(instance, serviceProvider, null);
            Validator.ValidateObject(instance, context, true);
        }
    }
}

#endif