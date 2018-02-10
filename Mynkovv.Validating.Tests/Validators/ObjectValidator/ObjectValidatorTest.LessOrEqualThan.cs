﻿using System;
using Xunit;

namespace Mynkovv.Validating.Tests.Validators.ObjectValidator
{
    public partial class ObjectValidatorTest
    {
        [Fact]
        public void LessOrEqualThan_2LessOrEqualThan3_Ok()
        {
            CreateObjectValidator(() => 2).LessOrEqualThan(3);
        }

        [Fact]
        public void LessOrEqualThan_3LessOrEqualThan3_Ok()
        {
            CreateObjectValidator(() => 3).LessOrEqualThan(3);
        }

        [Fact]
        public void LessOrEqualThan_4LessOrEqualThan3_ArgumentException()
        {
            int value3 = 3;
            int value4 = 4;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => value4).LessOrEqualThan(value3));
            Assert.Equal($"Object with name '{nameof(value4)}' must be less or equal than '{value3}'. Current value: '{value4}'", exc.Message);
        }

        [Fact]
        public void LessOrEqualThan_NullLessOrEqualThanNull_Ok()
        {
            object null1 = null;
            object null2 = null;
            CreateObjectValidator(() => null1).LessOrEqualThan(null2);
        }

        [Fact]
        public void LessOrEqualThan_EqualValuesNotImplementIComparable_Ok()
        {
            object notComparable = new object();
            CreateObjectValidator(() => notComparable).MoreOrEqualThan(notComparable);
        }

        [Fact]
        public void LessOrEqualThan_ValidatingObjectNotImplementIComparable_InvalidOperationException()
        {
            object notImplementIComparable = new object();
            object lessOrEqualThanValue = new object();
            InvalidOperationException exc = Assert.Throws<InvalidOperationException>(() => CreateObjectValidator(() => notImplementIComparable).LessOrEqualThan(lessOrEqualThanValue));
            Assert.Equal($"Object with name '{nameof(notImplementIComparable)}' not implement interface '{typeof(IComparable<object>)}'. Сannot compare objects", exc.Message);
        }
    }
}