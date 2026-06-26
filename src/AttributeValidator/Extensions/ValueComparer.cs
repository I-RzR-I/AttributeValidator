// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator
//  Author            : RzR
//  Created           : 25-06-2026 23:06
// 
//  Last Modified By : RzR
//  Last Modified On : 26-06-2026 20:22
//  ***********************************************************************
//  <copyright file="ValueComparer.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using System;
using System.Globalization;

#endregion

namespace RzR.Validation.Attributes.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Centralizes numeric, date, and timespan comparison for validation attributes.
    /// </summary>
    /// =================================================================================================
    internal static class ValueComparer
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Attempts to compare value to comparand and returns the sign of the comparison.
        /// </summary>
        /// <remarks>
        ///     Returns false (result = 0) if either argument is null, the type is unsupported,
        ///     or any conversion fails (fail-closed). Uses InvariantCulture for all conversions.
        ///     result &lt; 0 means value &lt; comparand; result == 0 means equal; result &gt; 0 means value &gt; comparand.
        /// </remarks>
        /// <param name="value">The value to compare.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="result">
        ///     When this method returns true, contains the sign of the comparison:
        ///     negative if value is less than comparand, zero if equal, positive if greater.
        ///     Set to 0 when the method returns false.
        /// </param>
        /// <returns>
        ///     True if the comparison succeeded; false if either argument is null,
        ///     the type is unsupported, or conversion fails.
        /// </returns>
        /// =================================================================================================
        internal static bool TryCompare(object value, object comparand, out int result)
        {
            result = 0;

            if (value == null || comparand == null)
                return false;

            var valueType = value.GetType().GetNonNullableType();
            var inv = CultureInfo.InvariantCulture;

            try
            {
                if (valueType.IsNumberNoPointType() || valueType.IsNumberUnSignedNoPointType() || valueType.IsNumberWithPointType())
                {
                    result = Convert.ToDecimal(value, inv).CompareTo(Convert.ToDecimal(comparand, inv));

                    return true;
                }

                if (valueType.IsDateType())
                {
                    var dateValue = value is DateTime dt ? dt : Convert.ToDateTime(value.ToString(), inv);
                    var dateComparand = comparand is DateTime dtc ? dtc : Convert.ToDateTime(comparand.ToString(), inv);
                    result = dateValue.CompareTo(dateComparand);

                    return true;
                }

                if (valueType.IsTimeSpanType())
                {
                    var tsValue = value is TimeSpan ts ? ts : TimeSpan.Parse(value.ToString(), inv);
                    var tsComparand = comparand is TimeSpan tsc ? tsc : TimeSpan.Parse(comparand.ToString(), inv);
                    result = tsValue.CompareTo(tsComparand);

                    return true;
                }
            }
            catch
            {
                result = 0;
                return false;
            }

            return false;
        }
    }
}