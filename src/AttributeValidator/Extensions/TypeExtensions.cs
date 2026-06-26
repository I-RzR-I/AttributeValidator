// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 26-06-2026 20:22
//  ***********************************************************************
//  <copyright file="TypeExtensions.cs" company="RzR SOFT & TECH">
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

#endregion

namespace RzR.Validation.Attributes.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A type extensions.
    /// </summary>
    /// Source: https://github.com/I-RzR-I/DomainCommonExtensions/blob/main/src/DomainCommonExtensions/CommonExtensions/TypeExtensions.cs
    /// =================================================================================================
    internal static class TypeExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that query if 'type' is nullable property type.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="type">The type to act on.</param>
        /// <returns>
        ///     True if nullable property type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNullablePropType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            try
            {
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }
            catch
            {
                return Nullable.GetUnderlyingType(type) != null;
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that gets non-nullable type.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="type">The type to act on.</param>
        /// <returns>
        ///     The non-nullable type.
        /// </returns>
        /// =================================================================================================
        internal static Type GetNonNullableType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that query if 'sourceType' is number no point type.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <returns>
        ///     True if number no point type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNumberNoPointType(this Type sourceType)
        {
            var type = sourceType.GetNonNullableType();

            var numberType = new List<Type> { typeof(sbyte), typeof(short), typeof(int), typeof(long) };

            return numberType.Contains(type);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that query if 'sourceType' is number un signed no point type.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <returns>
        ///     True if number un signed no point type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNumberUnSignedNoPointType(this Type sourceType)
        {
            var type = sourceType.GetNonNullableType();

            var numberType = new List<Type> { typeof(byte), typeof(ushort), typeof(uint), typeof(ulong) };

            return numberType.Contains(type);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that query if 'sourceType' is number with point type.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <returns>
        ///     True if number with point type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNumberWithPointType(this Type sourceType)
        {
            var type = sourceType.GetNonNullableType();

            var numberType = new List<Type> { typeof(float), typeof(decimal), typeof(double) };

            return numberType.Contains(type);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that query if 'sourceType' is date type.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <returns>
        ///     True if date type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsDateType(this Type sourceType)
        {
            var type = sourceType.GetNonNullableType();

            var numberType = new List<Type> { typeof(DateTime), typeof(DateTime?) };

            return numberType.Contains(type);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that query if 'sourceType' is time span type.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <returns>
        ///     True if time span type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsTimeSpanType(this Type sourceType)
        {
            var type = sourceType.GetNonNullableType();

            var numberType = new List<Type> { typeof(TimeSpan), typeof(TimeSpan?) };

            return numberType.Contains(type);
        }
    }
}