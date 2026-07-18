// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.AspNetCore
//  Author            : RzR
//  Created           : 18-07-2026 14:07
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:49
//  ***********************************************************************
//  <copyright file="RzRValidationOptions.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using Microsoft.AspNetCore.Http;
using System;

#endregion

namespace RzR.Validation.Attributes.AspNetCore.Options;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     Configuration options for the RzR validation middleware and filters. All options affect
///     both the Minimal API <see cref="Minimal.ValidationFilter{T}" />
///     and the MVC <see cref="Mvc.RzRValidateModelFilter" />.
/// </summary>
/// =================================================================================================
public class RzRValidationOptions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Gets or sets the HTTP status code returned when validation fails. Defaults to <c>400 Bad
    ///     Request</c>. Common alternative: <c>422 Unprocessable Entity</c>.
    /// </summary>
    /// <value>
    ///     The invalid status code.
    /// </value>
    /// =================================================================================================
    public int InvalidStatusCode { get; set; } = StatusCodes.Status400BadRequest;

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Gets or sets the <c>title</c> field of the RFC 7807 problem details response. Defaults to
    ///     "One or more validation errors occurred". Set to <c>null</c> to let the framework choose
    ///     a default title.
    /// </summary>
    /// <value>
    ///     The problem title.
    /// </value>
    /// =================================================================================================
    public string ProblemTitle { get; set; } = "One or more validation errors occurred.";

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Gets or sets the <c>type</c> URI of the RFC 7807 problem details response. When <c>null</c>
    ///     (the default) the framework's built-in type URI is used.
    /// </summary>
    /// <value>
    ///     The problem type URI.
    /// </value>
    /// =================================================================================================
    public string ProblemTypeUri { get; set; }

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Gets or sets an optional transformation applied to every member name key in the
    ///     <c>errors</c> dictionary of the problem details response.
    ///     Use this to normalise casing, e.g. <c>name =&gt; char.ToLowerInvariant(name[0]) +
    ///     name[1..]</c>
    ///     to convert <c>PascalCase</c> property names to <c>camelCase</c>. When <c>null</c> (the
    ///     default) member names are used as-is.
    /// </summary>
    /// <value>
    ///     A function delegate that yields a string.
    /// </value>
    /// =================================================================================================
    public Func<string, string> MemberNameTransformer { get; set; }
}