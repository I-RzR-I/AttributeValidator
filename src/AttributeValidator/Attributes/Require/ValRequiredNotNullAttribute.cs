// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.AttributeValidator
//  Author           : RzR
//  Created On       : 2024-04-23 17:20
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-24 20:33
// ***********************************************************************
//  <copyright file="ValRequiredNotNullAttribute.cs" company="">
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
    ///     Attribute for required not null.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValRequiredNotNullAttribute : ValidationAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValRequiredNotNullAttribute" /> class.
        /// </summary>
        /// =================================================================================================
        public ValRequiredNotNullAttribute() : base(Message.DefaultErrorMessage_NotNull) { }

#if NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        /// <inheritdoc />
        public override bool IsValid(object value) => value != null;
#endif

#if NETSTANDARD1_1
        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = string.Format(Message.DefaultErrorMessage_NotNull, validationContext.MemberName);

            return value != null ? ValidationResult.Success : new ValidationResult(message);
        }
#endif
    }
}