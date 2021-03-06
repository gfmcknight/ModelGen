﻿using System;
using System.Collections.Generic;
using System.Text;
using TranspilerDirectives;

namespace TestSamples
{
    [Transpile]
    public class Class6 : Class1
    {
        public int AdditionalProperty { get; set; }

        public new static object DefaultValue()
        {
            return new Class6
            {
                MyOtherProp = 3,
                AdditionalProperty = 11
            };
        }
    }
}
