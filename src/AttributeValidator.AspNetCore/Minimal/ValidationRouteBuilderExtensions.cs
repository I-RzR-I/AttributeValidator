// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.AspNetCore
//  Author            : RzR
//  Created           : 27-06-2026 01:06
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:52
//  ***********************************************************************
//  <copyright file="ValidationRouteBuilderExtensions.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

#endregion

namespace RzR.Validation.Attributes.AspNetCore.Minimal;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     Extension methods that attach <see cref="ValidationFilter{T}" /> to Minimal API route
///     builders using the fluent <c>WithValidation</c> convention.
/// </summary>
/// =================================================================================================
public static class ValidationRouteBuilderExtensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Adds a <see cref="ValidationFilter{T}" /> to the endpoint that validates the first bound
    ///     argument of type <typeparamref name="T" /> before the handler executes.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the bound parameter to validate. Must be a reference type.
    /// </typeparam>
    /// <param name="builder">The route handler builder to extend.</param>
    /// <returns>
    ///     The same <paramref name="builder" /> so calls can be chained.
    /// </returns>
    /// =================================================================================================
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
        => builder.AddEndpointFilter<ValidationFilter<T>>();

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Adds a <see cref="ValidationFilter{T}" /> to every endpoint in the group that validates
    ///     the first bound argument of type <typeparamref name="T" /> before the handler executes.
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the bound parameter to validate. Must be a reference type.
    /// </typeparam>
    /// <param name="builder">The route group builder to extend.</param>
    /// <returns>
    ///     The same <paramref name="builder" /> so calls can be chained.
    /// </returns>
    /// =================================================================================================
    public static RouteGroupBuilder WithValidation<T>(this RouteGroupBuilder builder) where T : class
        => builder.AddEndpointFilter<ValidationFilter<T>>();
}