// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.AttributeValidator
//  Author           : RzR
//  Created On       : 2024-04-23 13:02
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 13:02
// ***********************************************************************
//  <copyright file="Message.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace AttributeValidator.Resources
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A message resource.
    /// </summary>
    /// =================================================================================================
    internal static class Message
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not empty.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotEmpty = "The {0} field must not be empty!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not null.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotNull = "The {0} field must not be null!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message not default.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_NotDefault = "The {0} field must not be with default value!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message positive.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_Positive = "The {0} field must not be with value > 0!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default error message greater than.
        /// </summary>
        /// =================================================================================================
        internal const string DefaultErrorMessage_GreaterThan = "The {0} field must not be with value > {1}!";
    }
}