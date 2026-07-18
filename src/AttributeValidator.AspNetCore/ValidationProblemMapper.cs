// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.AspNetCore
//  Author            : RzR
//  Created           : 17-07-2026 23:07
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:45
//  ***********************************************************************
//  <copyright file="ValidationProblemMapper.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

namespace RzR.Validation.Attributes.AspNetCore;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     Maps a collection of <see cref="ValidationResult" /> failures to the
///     <c>errors</c> dictionary shape expected by <see cref="Microsoft.AspNetCore.Mvc.ValidationProblemDetails" />
///     and <see cref="Microsoft.AspNetCore.Http.Results.ValidationProblem" />.
/// </summary>
/// =================================================================================================
internal static class ValidationProblemMapper
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Converts validation results into a dictionary keyed by member name, where each value is a
    ///     deduplicated array of error messages for that member.
    /// </summary>
    /// <param name="results">The validation failures to map. Must not be <c>null</c>.</param>
    /// <param name="memberNameTransformer">
    ///     An optional function applied to each member name key before it is inserted into the
    ///     dictionary (e.g. to convert <c>PascalCase</c> to <c>camelCase</c>). When <c>null</c> the
    ///     key is used as-is.
    /// </param>
    /// <returns>
    ///     A dictionary where each key is a (optionally transformed) member name and each value is a
    ///     non-empty array of distinct error messages. Results with no member names use an empty
    ///     string as the key.
    /// </returns>
    /// =================================================================================================
    internal static IDictionary<string, string[]> ToErrorDictionary(IEnumerable<ValidationResult> results,
        Func<string, string> memberNameTransformer)
    {
        var grouped = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);

        foreach (var result in results)
        {
            var message = result.ErrorMessage ?? string.Empty;
            var memberNames = result.MemberNames?.ToList() ?? [];

            if (memberNames.Count == 0) memberNames.Add(string.Empty);

            foreach (var rawKey in memberNames)
            {
                var key = memberNameTransformer != null && rawKey.Length > 0
                    ? memberNameTransformer(rawKey)
                    : rawKey;

                if (!grouped.TryGetValue(key, out var messages))
                {
                    messages = new HashSet<string>(StringComparer.Ordinal);
                    grouped[key] = messages;
                }

                messages.Add(message);
            }
        }

        return grouped.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToArray(),
            StringComparer.Ordinal);
    }
}