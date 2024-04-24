﻿// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 22:11
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 22:11
// ***********************************************************************
//  <copyright file="NotEmptyNullableModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AttributeValidator.Attributes.Require;
using System;

namespace ModelAttributeValidationTests.Models.NotEmpty
{
    public class NotEmptyNullableModel
    {
        [ValRequiredNotEmpty]
        public int? Id { get; set; }

        [ValRequiredNotEmpty]
        public string Name { get; set; }

        [ValRequiredNotEmpty]
        public string Code { get; set; }

        [ValRequiredNotEmpty]
        public bool? IsActive { get; set; }

        [ValRequiredNotEmpty]
        public Guid? GId { get; set; }
    }
}