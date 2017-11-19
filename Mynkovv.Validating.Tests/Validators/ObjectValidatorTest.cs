﻿using Mynkovv.Validating.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace Mynkovv.Validating.Tests.Validators
{
    public class ObjectValidatorTest
    {
        // Default

        [Fact]
        public void Default_ReferenceTypeIsNull_Ok()
        {
            object arg = null;
            CreateObjectValidator(() => arg).Default();
        }

        [Fact]
        public void Default_ReferenceTypeIsNotNull_Exception()
        {
            object arg = new object();
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => arg).Default());
            Assert.Equal($"Object with name '{nameof(arg)}' must be default value. Current value: '{arg}'", exc.Message);
        }

        [Fact]
        public void Default_ValueTypeIsDefault_Ok()
        {
            CreateObjectValidator(() => default(int)).Default();
        }

        [Fact]
        public void Default_ValueTypeIsNotDefault_Exception()
        {
            int arg = 5;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => arg).Default());
            Assert.Equal($"Object with name '{nameof(arg)}' must be default value. Current value: '{arg}'", exc.Message);
        }

        // NotDefault

        [Fact]
        public void NotDefault_ReferenceTypeIsNotNull_Ok()
        {
            CreateObjectValidator(() => new object()).NotDefault();
        }

        [Fact]
        public void NotDefault_ReferenceTypeIsNull_Exception()
        {
            object arg = null;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => arg).NotDefault());
            Assert.Equal($"Object with name '{nameof(arg)}' must be not default value", exc.Message);
        }

        [Fact]
        public void NotDefault_ValueTypeIsNotDefault_Ok()
        {
            CreateObjectValidator(() => 5).NotDefault();
        }

        [Fact]
        public void NotDefault_ValueTypeIsDefault_Exception()
        {
            int arg = default(int);
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => arg).NotDefault());
            Assert.Equal($"Object with name '{nameof(arg)}' must be not default value", exc.Message);
        }

        // Null

        [Fact]
        public void Null_ObjectIsNotNull_Exception()
        {
            object notNullObj = new object();
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => notNullObj).Null());
            Assert.Equal($"Object with name '{nameof(notNullObj)}' must be null. Current value: '{notNullObj}'", exc.Message);
        }

        [Fact]
        public void Null_ObjectIsNull_Ok()
        {
            object nullObj = null;
            CreateObjectValidator(() => nullObj).Null();
        }

        [Fact]
        public void Null_NullableIsNotNull_Exception()
        {
            int? notNullObj = 5;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => notNullObj).Null());
            Assert.Equal($"Object with name '{nameof(notNullObj)}' must be null. Current value: '{notNullObj}'", exc.Message);
        }

        [Fact]
        public void Null_NullableIsNull_Ok()
        {
            int? nullObj = null;
            CreateObjectValidator(() => nullObj).Null();
        }

        // NotNull

        [Fact]
        public void NotNull_ObjectIsNull_Exception()
        {
            object nullObj = null;
            ArgumentNullException exc = Assert.Throws<ArgumentNullException>(() => CreateObjectValidator(() => nullObj).NotNull());
            Assert.Equal(nameof(nullObj), exc.ParamName);
        }

        [Fact]
        public void NotNull_ObjectIsNotNull_Ok()
        {
            CreateObjectValidator(() => new object()).NotNull();
        }

        [Fact]
        public void NotNull_NullableIsNull_Exception()
        {
            int? nullArg = null;
            ArgumentNullException exc = Assert.Throws<ArgumentNullException>(() => CreateObjectValidator(() => nullArg).NotNull());
            Assert.Equal(nameof(nullArg), exc.ParamName);
        }

        [Fact]
        public void NotNull_NullableIsNotNull_Ok()
        {
            CreateObjectValidator(() => 5).NotNull();
        }

        // Equal

        [Fact]
        public void Equal_5Equal5_Ok()
        {
            CreateObjectValidator(() => 5).Equal(5);
        }

        [Fact]
        public void Equal_5NotEqual4_Exception()
        {
            int val = 5;
            int expectedVal = 4;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => val).Equal(expectedVal));
            Assert.Equal($"Object with name '{nameof(val)}' must be equal '{expectedVal}'. Current value: '{val}'", exc.Message);
        }

        // NotEqual

        [Fact]
        public void NotEqual_IntNotEqual_Ok()
        {
            CreateObjectValidator(() => 5).NotEqual(4);
        }

        [Fact]
        public void NotEqual_IntEqual_Exception()
        {
            int val = 5;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => val).NotEqual(val));
            Assert.Equal($"Object with name '{nameof(val)}' must be not equal '{val}'", exc.Message);
        }

        // MoreThan

        [Fact]
        public void MoreThan_2MoreThan1_Ok()
        {
            CreateObjectValidator(() => 2).MoreThan(1);
        }

        [Fact]
        public void MoreThan_0MoreThan1_Exception()
        {
            int argEqual0 = 0;
            int valueEqual1 = 1;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => argEqual0).MoreThan(valueEqual1));
            Assert.Equal($"Object with name '{nameof(argEqual0)}' must be more than '{valueEqual1}'", exc.Message);
        }

        // MoreOrEqualThan

        [Fact]
        public void MoreOrEqualThan_3MoreOrEqualThan2_Ok()
        {
            CreateObjectValidator(() => 3).MoreOrEqualThan(2);
        }

        [Fact]
        public void MoreOrEqualThan_3MoreOrEqualThan3_Ok()
        {
            CreateObjectValidator(() => 3).MoreOrEqualThan(3);
        }

        [Fact]
        public void MoreOrEqualThan_3MoreOrEqualThan4_Exception()
        {
            int value3 = 3;
            int value4 = 4;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => value3).MoreOrEqualThan(value4));
            Assert.Equal($"Object with name '{nameof(value3)}' must be more or equal than '{value4}'. Current value: '{value3}'", exc.Message);
        }

        [Fact]
        public void MoreOrEqualThan_NullMoreOrEqualThanNull_Ok()
        {
            object null1 = null;
            object null2 = null;
            CreateObjectValidator(() => null1).MoreOrEqualThan(null2);
        }

        [Fact]
        public void MoreOrEqualThan_BothValuesNotImplementIComparable()
        {
            object null1 = new object();
            CreateObjectValidator(() => null1).MoreOrEqualThan(null1);
        }

        // LessThan

        [Fact]
        public void LessThan_3LessThan4_Ok()
        {
            CreateObjectValidator(() => 3).LessThan(4);
        }

        [Fact]
        public void LessThan_EqualValues_Exception()
        {
            int val = 5;
            int equalVal = val;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => val).LessThan(equalVal));
            Assert.Equal($"Object with name '{nameof(val)}' must be less than '{equalVal}'. Current value: '{val}'", exc.Message);
        }

        [Fact]
        public void LessThan_BigValueLessThanSmallValue_Exception()
        {
            int bigValue = 5;
            int smallValue = bigValue - 1;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => CreateObjectValidator(() => bigValue).LessThan(smallValue));
            Assert.Equal($"Object with name '{nameof(bigValue)}' must be less than '{smallValue}'. Current value: '{bigValue}'", exc.Message);
        }





        // LessOrEqualThan

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
        public void LessOrEqualThan_4LessOrEqualThan3_Exception()
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
        public void LessOrEqualThan_BothValuesNotImplementIComparable()
        {
            object null1 = new object();
            CreateObjectValidator(() => null1).MoreOrEqualThan(null1);
        }

        private static ObjectValidator<T> CreateObjectValidator<T>(Expression<Func<T>> arg)
        {
            var validatingObject = Validate.CreateValidatingObjectFromExpression(arg);
            return new ObjectValidator<T>(validatingObject);
        }
    }
}