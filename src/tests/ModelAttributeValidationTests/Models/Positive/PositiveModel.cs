// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-23 22:40
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-23 22:40
// ***********************************************************************
//  <copyright file="PositiveModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AttributeValidator.Attributes.Require;

namespace ModelAttributeValidationTests.Models.Positive
{
    public class PositiveModel
    {
        public int IntNumber { get; set; }

        [ValRequiredPositive]
        public long LongNumber { get; set; }

        [ValRequiredPositive]
        public short ShortNumber { get; set; }

        [ValRequiredPositive]
        public ushort UnSignedShortNumber { get; set; }

        [ValRequiredPositive]
        public decimal DecimalNumber { get; set; }

        [ValRequiredPositive]
        public float FloatNumber { get; set; }

        [ValRequiredPositive]
        public double DoubleNumber { get; set; }
    }
}