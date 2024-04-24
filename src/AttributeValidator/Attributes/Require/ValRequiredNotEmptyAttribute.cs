// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.AttributeValidator
//  Author           : RzR
//  Created On       : 2024-04-23 17:20
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-24 20:33
// ***********************************************************************
//  <copyright file="ValRequiredNotEmptyAttribute.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AttributeValidator.Resources;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace AttributeValidator.Attributes.Require
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for required not empty.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValRequiredNotEmptyAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValRequiredNotEmptyAttribute" /> class.
        /// </summary>
        /// =================================================================================================
        public ValRequiredNotEmptyAttribute() : base(Message.DefaultErrorMessage_NotEmpty) { }

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc />
        public override bool IsValid(object value)
            => ValidateObject(value);
#endif

#if NETSTANDARD1_1
        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = string.Format(Message.DefaultErrorMessage_NotEmpty, validationContext.MemberName);

            return ValidateObject(value) ? ValidationResult.Success : new ValidationResult(message);
        }
#endif

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
            switch (value)
            {
                case Guid guid:
                    return guid != Guid.Empty;
                case string stringParam:
                    return !string.IsNullOrEmpty(stringParam.Trim());
                default:
                    return true;
            }
        }
    }
}