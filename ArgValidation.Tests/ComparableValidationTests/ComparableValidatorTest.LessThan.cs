﻿using System;
using Xunit;

namespace ArgValidation.Tests.ComparableValidationTests
{
    public partial class ComparableValidatorTest
    {
        [Fact]
        public void LessThan_3LessThan4_Ok()
        {
            Arg.Validate(() => 3).LessThan(4);
        }

        [Fact]
        public void LessThan_EqualValues_ArgumentOutOfRangeException()
        {
            int val = 5;
            int equalVal = val;
            ArgumentOutOfRangeException exc = Assert.Throws<ArgumentOutOfRangeException>(() => Arg.Validate(() => val).LessThan(equalVal));
            Assert.Equal($"Argument '{nameof(val)}' must be less than '{equalVal}'. Current value: '{val}'", exc.Message);
        }

        [Fact]
        public void LessThan_5LessThan4_ArgumentOutOfRangeException()
        {
            int value5 = 5;
            int value4 = 4;
            ArgumentOutOfRangeException exc = Assert.Throws<ArgumentOutOfRangeException>(() => Arg.Validate(() => value5).LessThan(value4));
            Assert.Equal($"Argument '{nameof(value5)}' must be less than '{value4}'. Current value: '{value5}'", exc.Message);
        }

        [Fact]
        public void LessThan_ArgumentIsNull_ArgValidationException()
        {
            ComparableClass nullValue = null;
            ComparableClass lessThanValue = new ComparableClass();
            ArgValidationException exc = Assert.Throws<ArgValidationException>(() =>
            {
                Arg.Validate(() => nullValue).LessThan(lessThanValue);
            });
            Assert.Equal($"Argument '{nameof(nullValue)}' is null. Сan not compare null object", exc.Message);
        }

        [Fact]
        public void LessThan_LessThanArgumentIsNull_ArgValidationException()
        {
            ComparableClass value = new ComparableClass();
            ComparableClass lessThanNull = null;
            ArgValidationException exc = Assert.Throws<ArgValidationException>(() =>
            {
                Arg.Validate(() => value).LessThan(lessThanNull);
            });
            Assert.Equal($"Argument 'value' of method 'LessThan' is null. Сan not compare null object", exc.Message);
        }

        [Fact]
        public void LessThan_ValidationIsDisabled_WithoutException()
        {
            int lessThan = 1;
            var arg = new Argument<int>(lessThan, "name", validationIsDisabled: true);
            arg.LessThan(lessThan);
        }

        [Fact]
        public void LessThan_WithCustomException_CustomTypeException()
        {
            int value3 = 3;

            CustomException exc = Assert.Throws<CustomException>(() =>
                Arg.Validate(value3, nameof(value3))
                    .With<CustomException>()
                    .LessThan(2));

            Assert.Equal($"Argument '{nameof(value3)}' must be less than '2'. Current value: '{value3}'",
                exc.Message);
        }
    }
}
