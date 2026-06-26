// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 26-06-2026 20:22
//  ***********************************************************************
//  <copyright file="MemberHelper.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using RzR.Validation.Attributes.Common;
using System;
using System.Reflection;

#endregion

namespace RzR.Validation.Attributes.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Provides helpers for reading property/field values from an object instance and
    ///     evaluating comparison operators against those values.
    /// </summary>
    /// =================================================================================================
    internal static class MemberHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Returns the value of the named property or field on <paramref name="instance" />.
        /// </summary>
        /// <remarks>
        ///     Property is checked before field. Uses GetRuntimeProperty/GetRuntimeField so the
        ///     call works identically across net45, netstandard1.1, and netstandard2.0.
        ///     Returns null when <paramref name="instance" /> is null, <paramref name="memberName" />
        ///     is null or empty, or no matching member is found.
        /// </remarks>
        /// <param name="instance">The object whose member value is to be read.</param>
        /// <param name="memberName">The name of the property or field to read.</param>
        /// <returns>
        ///     The member value, or null if the member cannot be resolved.
        /// </returns>
        /// =================================================================================================
        internal static object GetMemberValue(object instance, string memberName)
        {
            if (instance == null || string.IsNullOrEmpty(memberName))
                return null;

            var type = instance.GetType();

            var prop = type.GetRuntimeProperty(memberName);
            if (prop != null)
                return prop.GetValue(instance);

            var field = type.GetRuntimeField(memberName);
            if (field != null)
                return field.GetValue(instance);

            return null;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Returns true when <paramref name="value" /> is considered populated (non-null and non-empty).
        /// </summary>
        /// <remarks>
        ///     Null is never populated. A string must be non-null and non-whitespace.
        ///     A <see cref="Guid" /> must be non-empty. All other types are considered populated
        ///     when they are not null.
        /// </remarks>
        /// <param name="value">The value to inspect.</param>
        /// <returns>
        ///     True if the value is considered populated; false otherwise.
        /// </returns>
        /// =================================================================================================
        internal static bool IsPopulated(object value)
        {
            if (value == null)
                return false;

            if (value is string s)
                return !string.IsNullOrWhiteSpace(s);

            if (value is Guid g)
                return g != Guid.Empty;

            return true;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Evaluates <paramref name="op" /> between <paramref name="left" /> and
        ///     <paramref name="right" />, returning the boolean result.
        /// </summary>
        /// <remarks>
        ///     Equality operators delegate to <see cref="object.Equals(object, object)" />.
        ///     Ordering operators delegate to <see cref="ValueComparer.TryCompare" />; if that
        ///     method returns false (unsupported type or null argument) the evaluation returns false.
        /// </remarks>
        /// <param name="left">The left-hand operand.</param>
        /// <param name="op">The comparison operator to apply.</param>
        /// <param name="right">The right-hand operand.</param>
        /// <returns>
        ///     The boolean result of applying <paramref name="op" /> to the two operands,
        ///     or false if the comparison cannot be performed.
        /// </returns>
        /// =================================================================================================
        internal static bool Evaluate(object left, ValOp op, object right)
        {
            switch (op)
            {
                case ValOp.Equals:
                    return AreEqual(left, right);
                case ValOp.NotEquals:
                    return !AreEqual(left, right);
                default:
                    if (!ValueComparer.TryCompare(left, right, out var c))
                        return false;

                    switch (op)
                    {
                        case ValOp.GreaterThan: return c > 0;
                        case ValOp.GreaterThanOrEqual: return c >= 0;
                        case ValOp.LessThan: return c < 0;
                        case ValOp.LessThanOrEqual: return c <= 0;
                        default: return false;
                    }
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Value-aware equality. For comparable types (numeric/date/timespan) equality is decided by
        ///     <see cref="ValueComparer.TryCompare" /> so that values of different widths (e.g. an
        ///     <c>int</c> and a <c>long</c>) compare equal; otherwise it falls back to
        ///     <see cref="object.Equals(object, object)" /> (handles strings, enums, and nulls).
        /// </summary>
        /// <param name="left">The left-hand operand.</param>
        /// <param name="right">The right-hand operand.</param>
        /// <returns>True when the two operands are considered equal.</returns>
        /// =================================================================================================
        internal static bool AreEqual(object left, object right)
            => ValueComparer.TryCompare(left, right, out var c)
                ? c == 0
                : Equals(left, right);
    }
}