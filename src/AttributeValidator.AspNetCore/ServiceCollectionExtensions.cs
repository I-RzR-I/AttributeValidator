// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.AspNetCore
//  Author            : RzR
//  Created           : 17-07-2026 23:07
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:47
//  ***********************************************************************
//  <copyright file="ServiceCollectionExtensions.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using Microsoft.Extensions.DependencyInjection;
using RzR.Validation.Attributes.AspNetCore.Options;
using System;

#endregion

namespace RzR.Validation.Attributes.AspNetCore;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     Extension methods for registering RzR validation services in an
///     <see cref="IServiceCollection" />.
/// </summary>
/// =================================================================================================
public static class ServiceCollectionExtensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Registers <see cref="RzRValidationOptions" /> with default values.
    /// </summary>
    /// <param name="services">The service collection to add registration to.</param>
    /// <returns>
    ///     The same <paramref name="services" /> so calls can be chained.
    /// </returns>
    /// =================================================================================================
    public static IServiceCollection AddRzRValidation(this IServiceCollection services)
        => services.AddRzRValidation(_ => { });

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Registers <see cref="RzRValidationOptions" /> and applies the supplied
    ///     <paramref name="configure" /> delegate so callers can override defaults.
    /// </summary>
    /// <param name="services">The service collection to add registration to.</param>
    /// <param name="configure">
    ///     A delegate that configures the <see cref="RzRValidationOptions" /> instance.
    /// </param>
    /// <returns>
    ///     The same <paramref name="services" /> so calls can be chained.
    /// </returns>
    /// =================================================================================================
    public static IServiceCollection AddRzRValidation(this IServiceCollection services,
        Action<RzRValidationOptions> configure)
    {
        services.Configure(configure);

        return services;
    }
}