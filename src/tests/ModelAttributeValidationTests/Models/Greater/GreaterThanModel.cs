// ***********************************************************************
//  Assembly         : RzR.Shared.Attributes.ModelAttributeValidationTests
//  Author           : RzR
//  Created On       : 2024-04-24 17:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-24 17:16
// ***********************************************************************
//  <copyright file="GreaterThanModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AttributeValidator.Attributes.Greater;

namespace ModelAttributeValidationTests.Models.Greater
{
    public class GreaterThanModel
    {
        public int IntNumber { get; set; }

        [ValGreaterThan(5)]
        public long LongNumber { get; set; }

        [ValGreaterThan(10)]
        public short ShortNumber { get; set; }

        [ValGreaterThan(1)]
        public ushort UnSignedShortNumber { get; set; }

        [ValGreaterThan(5.69)]
        public decimal DecimalNumber { get; set; }

        [ValGreaterThan(6.9)]
        public float FloatNumber { get; set; }

        [ValGreaterThan(56.9)]
        public double DoubleNumber { get; set; }
    }
}