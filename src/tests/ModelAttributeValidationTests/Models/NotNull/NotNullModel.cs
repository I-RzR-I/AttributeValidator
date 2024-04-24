// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 21:32
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 21:32
// ***********************************************************************
//  <copyright file="NotNullModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AttributeValidator.Attributes.Require;

namespace ModelAttributeValidationTests.Models.NotNull
{
    public class NotNullModel
    {
        [ValRequiredNotNull]
        public int Id { get; set; }

        [ValRequiredNotNull]
        public string Name { get; set; }

        [ValRequiredNotNull]
        public string Code { get; set; }

        [ValRequiredNotNull]
        public bool IsActive { get; set; }
    }
}