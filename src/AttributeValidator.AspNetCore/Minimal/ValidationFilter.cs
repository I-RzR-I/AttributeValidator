// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.AspNetCore
//  Author            : RzR
//  Created           : 17-07-2026 23:07
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:53
//  ***********************************************************************
//  <copyright file="ValidationFilter.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RzR.Validation.Attributes.AspNetCore.Options;
using RzR.Validation.Attributes.Extensions;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace RzR.Validation.Attributes.AspNetCore.Minimal;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     An <see cref="IEndpointFilter" /> that validates the first bound argument of type
///     <typeparamref name="T" /> using the core <c>RzR.Validation.Attributes</c> library.
///     On failure the filter short-circuits the request pipeline and returns an RFC 7807
///     <c>ValidationProblem</c> response. On success, it forwards the request to the next
///     filter or endpoint handler.
/// </summary>
/// <typeparam name="T">
///     The type of the bound parameter to validate. Must be a reference type.
/// </typeparam>
/// <seealso cref="T:Microsoft.AspNetCore.Http.IEndpointFilter"/>
/// =================================================================================================
public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly ILogger<ValidationFilter<T>> _logger;
    private readonly RzRValidationOptions _options;

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Initializes a new instance of <see cref="ValidationFilter{T}" />.
    /// </summary>
    /// <param name="options">The resolved validation options.</param>
    /// <param name="logger">Logger for debug-level short-circuit events.</param>
    /// =================================================================================================
    public ValidationFilter(IOptions<RzRValidationOptions> options, ILogger<ValidationFilter<T>> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var target = context.Arguments.OfType<T>().FirstOrDefault();

        if (target == null) return await next(context);

        var isValid = target.TryValidate(out var results, context.HttpContext.RequestServices);

        if (!isValid)
        {
            var errors = ValidationProblemMapper.ToErrorDictionary(results, _options.MemberNameTransformer);

            _logger.LogDebug(
                "Validation failed for {TypeName}. Failing members: {Members}",
                typeof(T).Name,
                string.Join(", ", errors.Keys));

            return Results.ValidationProblem(
                errors,
                title: _options.ProblemTitle,
                type: _options.ProblemTypeUri,
                statusCode: _options.InvalidStatusCode);
        }

        return await next(context);
    }
}