// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.AttributeValidator
//  Author           : RzR
//  Created On       : 2024-04-23 18:22
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 19:29
// ***********************************************************************
//  <copyright file="TypeExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using CodeSource;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AttributeValidator.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A type extensions.
    /// </summary>
    /// =================================================================================================
    [CodeSource("https://github.com/I-RzR-I/DomainCommonExtensions/blob/main/src/DomainCommonExtensions/CommonExtensions/TypeExtensions.cs", "RzR", 1.0D)]
    internal static class TypeExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) dictionary of nullable types.
        /// </summary>
        /// =================================================================================================
        private static readonly Dictionary<Type, Type> NullableTypeDict = new Dictionary<Type, Type>
        {
            [typeof(byte?)] = typeof(byte),
            [typeof(sbyte?)] = typeof(sbyte),
            [typeof(short?)] = typeof(short),
            [typeof(ushort?)] = typeof(ushort),
            [typeof(int?)] = typeof(int),
            [typeof(uint?)] = typeof(uint),
            [typeof(long?)] = typeof(long),
            [typeof(ulong?)] = typeof(ulong),
            [typeof(float?)] = typeof(float),
            [typeof(double?)] = typeof(double),
            [typeof(decimal?)] = typeof(decimal),
            [typeof(bool?)] = typeof(bool),
            [typeof(char?)] = typeof(char),
            [typeof(Guid?)] = typeof(Guid),
            [typeof(DateTime?)] = typeof(DateTime),
            [typeof(DateTimeOffset?)] = typeof(DateTimeOffset),
            [typeof(TimeSpan?)] = typeof(TimeSpan)
        };

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
        ///     A Type extension method that gets non nullable type.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="type">The type to act on.</param>
        /// <returns>
        ///     The non nullable type.
        /// </returns>
        /// =================================================================================================
        internal static Type GetNonNullableType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsNullablePropType()
                ? NullableTypeDict.FirstOrDefault(x => x.Key == type).Value
                : type;
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

            var numberType = new List<Type> { typeof(int), typeof(nint), typeof(short), typeof(long) };

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

            var numberType = new List<Type>
            {
                typeof(int),
                typeof(nint),
                typeof(short),
                typeof(ushort),
                typeof(ulong)
            };

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