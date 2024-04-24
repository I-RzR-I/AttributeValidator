// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 21:42
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 21:42
// ***********************************************************************
//  <copyright file="NotNullNullableModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AttributeValidator.Attributes.Require;

namespace ModelAttributeValidationTests.Models.NotNull
{
    public class NotNullNullableModel
    {
        [ValRequiredNotNull]
        public int? Id { get; set; }

        [ValRequiredNotNull]
        public string Name { get; set; }

        [ValRequiredNotNull]
        public string Code { get; set; }

        [ValRequiredNotNull]
        public bool? IsActive { get; set; }
    }
}