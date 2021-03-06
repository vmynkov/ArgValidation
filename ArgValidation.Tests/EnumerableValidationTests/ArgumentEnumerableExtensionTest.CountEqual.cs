﻿using System;
using Xunit;

namespace ArgValidation.Tests.EnumerableValidationTests
{
    public partial class ArgumentEnumerableExtensionTest
    {
        [Fact]
        public void CountEqual_ValuesIsNull_ArgValidationException()
        {
            object[] nullValue = null;
            ArgValidationException exc = Assert.Throws<ArgValidationException>(() =>
                Arg.Validate(() => nullValue)
                    .CountEqual(0));
            Assert.Equal($"Argument '{nameof(nullValue)}' is null. Сan not get count elements from null object", exc.Message);
        }

        [Fact]
        public void CountEqual_CountEqual_Ok()
        {
            object[] objs = new[] { new object(), new object() };

            Arg.Validate(() => objs).CountEqual(objs.Length);
        }

        [Fact]
        public void CountEqual_CountNotEqual_ArgumentException()
        {
            object[] objs = { new object(), new object() };
            int count = objs.Length + 1;

            ArgumentException exc = Assert.Throws<ArgumentException>(() =>
                Arg.Validate(() => objs)
                    .CountEqual(count));

            Assert.Equal($"Argument '{nameof(objs)}' must contains {count} elements. Current count elements: {objs.Length}", exc.Message);
        }

        [Fact]
        public void CountEqual_ValidationIsDisabled_WithoutException()
        {
            int[] digits = { 1, 2 };
            var arg = new Argument<int[]>(digits, "name", validationIsDisabled: true);

            arg.CountEqual(digits.Length + 1);
        }

        [Fact]
        public void CountEqual_WithCustomException_CustomTypeException()
        {
            int[] arr = { 1 };

            CustomException exc = Assert.Throws<CustomException>(() =>
                Arg.Validate(arr, nameof(arr))
                    .With<CustomException>()
                    .CountEqual(2));

            Assert.Equal($"Argument '{nameof(arr)}' must contains 2 elements. Current count elements: {arr.Length}", exc.Message);
        }
    }
}
