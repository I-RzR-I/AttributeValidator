// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.AspNetCore
//  Author            : RzR
//  Created           : 17-07-2026 23:07
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:51
//  ***********************************************************************
//  <copyright file="ValidateModelAttribute.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

namespace RzR.Validation.Attributes.AspNetCore.Mvc;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     Applies <see cref="RzRValidateModelFilter" /> to the decorated controller class or action
///     method, returning a uniform RFC 7807 <see cref="Microsoft.AspNetCore.Mvc.ValidationProblemDetails" />
///     response when <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary.IsValid" />
///     is <c>false</c>.
/// </summary>
/// <remarks>
///     Use this attribute for per-controller or per-action opt-in validation. For global
///     registration, add <see cref="RzRValidateModelFilter" /> directly to
///     <c>MvcOptions.Filters</c> in <c>AddControllers()</c> instead.
/// </remarks>
/// <seealso cref="T:Microsoft.AspNetCore.Mvc.TypeFilterAttribute"/>
/// =================================================================================================
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ValidateModelAttribute : TypeFilterAttribute
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Initializes a new instance of <see cref="ValidateModelAttribute" />,
    ///      which activates <see cref="RzRValidateModelFilter" /> via the DI container.
    /// </summary>
    /// =================================================================================================
    public ValidateModelAttribute() : base(typeof(RzRValidateModelFilter))
    {
    }
}