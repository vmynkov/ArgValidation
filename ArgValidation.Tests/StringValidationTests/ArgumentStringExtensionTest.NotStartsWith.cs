﻿using System;
using Xunit;

namespace ArgValidation.Tests.StringValidationTests
{
    public partial class ArgumentStringExtensionTest
    {
        [Fact]
        public void NotStartsWith_ArgumentIsNull_ArgValidationException()
        {
            string nullArg = null;
            var exc = Assert.Throws<ArgValidationException>(() => Arg.Validate(() => nullArg).NotStartsWith(""));
            Assert.Equal($"Argument '{nameof(nullArg)}' is null. Can not execute 'NotStartsWith' method", exc.Message);
        }

        [Fact]
        public void NotStartsWith_ValueIsNull_ArgValidationException()
        {
            ArgValidationException exc = Assert.Throws<ArgValidationException>(() =>
            {
                Arg.Validate(() => "value").NotStartsWith(null);
            });
            Assert.Equal("Argument 'value' of method 'NotStartsWith' is null. Can not execute 'NotStartsWith' method", exc.Message);
        }

        [Fact]
        public void NotStartsWith_ArgumentNotStartsWithValue_Ok()
        {
            string arg = "qwe";
            string value = "123";
            Arg.Validate(() => arg).NotStartsWith(value);
        }

        [Fact]
        public void NotStartsWith_ArgumentStartsWithValue_ArgumentException()
        {
            string arg = "string";
            string value = "str";
            ArgumentException exc = Assert.Throws<ArgumentException>(() => Arg.Validate(() => arg).NotStartsWith(value));
            Assert.Equal($"Argument '{nameof(arg)}' must not starts with '{value}'. Current value: '{arg}'", exc.Message);
        }

        [Fact]
        public void NotStartsWith_ArgumentStartsWithValueWithIgnoreCase_ArgumentException()
        {
            string arg = "string";
            string value = "STR";
            ArgumentException exc = Assert.Throws<ArgumentException>(() => Arg.Validate(() => arg).NotStartsWith(value, StringComparison.OrdinalIgnoreCase));
            Assert.Equal($"Argument '{nameof(arg)}' must not starts with '{value}'. Current value: '{arg}'", exc.Message);
        }

        [Fact]
        public void NotStartsWith_ValidationIsDisabled_WithoutException()
        {
            string value = "asdf";
            var arg = new Argument<string>(value, "name", validationIsDisabled: true);
            arg.NotStartsWith(value);
        }

        [Fact]
        public void NotStartsWith_WithCustomException_CustomTypeException()
        {
            string value = "123";

            CustomException exc = Assert.Throws<CustomException>(() =>
                Arg.Validate(value, nameof(value))
                    .With<CustomException>()
                    .NotStartsWith("12"));

            Assert.Equal($"Argument '{nameof(value)}' must not starts with '12'. Current value: '{value}'", exc.Message);
        }
    }
}
