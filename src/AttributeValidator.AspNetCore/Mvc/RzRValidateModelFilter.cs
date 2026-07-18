// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.AspNetCore
//  Author            : RzR
//  Created           : 17-07-2026 23:07
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:51
//  ***********************************************************************
//  <copyright file="RzRValidateModelFilter.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RzR.Validation.Attributes.AspNetCore.Options;
using System.Linq;

#endregion

namespace RzR.Validation.Attributes.AspNetCore.Mvc;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     An MVC action filter that short-circuits the request pipeline when
///     <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary.IsValid" /> is
///     <c>false</c>, returning a uniform RFC 7807 <see cref="ValidationProblemDetails" />
///     response.
///     MVC model binding already executes DataAnnotations attributes; this filter standardizes
///     the error response shape so every controller receives the same 400 body without
///     boilerplate.
/// </summary>
/// <remarks>
///     Register globally via <c>services.AddControllers(o =&gt; o.Filters.Add&lt;
///     RzRValidateModelFilter&gt;())</c>
///     or per-controller/action via <see cref="ValidateModelAttribute" />.
/// </remarks>
/// <seealso cref="T:Microsoft.AspNetCore.Mvc.Filters.IActionFilter"/>
/// =================================================================================================
public sealed class RzRValidateModelFilter : IActionFilter
{
    private readonly ILogger<RzRValidateModelFilter> _logger;
    private readonly RzRValidationOptions _options;

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Initializes a new instance of <see cref="RzRValidateModelFilter" />.
    /// </summary>
    /// <param name="options">The resolved validation options.</param>
    /// <param name="logger">Logger for debug-level short-circuit events.</param>
    /// =================================================================================================
    public RzRValidateModelFilter(IOptions<RzRValidationOptions> options, ILogger<RzRValidateModelFilter> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc/>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var failingKeys = context.ModelState
            .Where(kvp => kvp.Value?.Errors.Count > 0)
            .Select(kvp => kvp.Key)
            .ToList();

        _logger.LogDebug(
            "ModelState invalid. Failing members: {Members}",
            string.Join(", ", failingKeys));

        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = _options.InvalidStatusCode,
            Title = _options.ProblemTitle
        };

        if (_options.ProblemTypeUri != null) 
            problemDetails.Type = _options.ProblemTypeUri;

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = _options.InvalidStatusCode, 
            ContentTypes =
            {
                new MediaTypeHeaderValue("application/problem+json")
            }
        };
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}