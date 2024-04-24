// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 20:39
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 20:39
// ***********************************************************************
//  <copyright file="NotDefaultModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AttributeValidator.Attributes.Require;

namespace ModelAttributeValidationTests.Models.NotDefault
{
    public class NotDefaultModel
    {
        [ValRequiredNotDefault]
        public int Id { get; set; }
        
        [ValRequiredNotDefault]
        public string Name { get; set; }
        
        [ValRequiredNotDefault]
        public string Code { get; set; }

        [ValRequiredNotDefault]
        public bool IsActive { get; set; }
    }
}