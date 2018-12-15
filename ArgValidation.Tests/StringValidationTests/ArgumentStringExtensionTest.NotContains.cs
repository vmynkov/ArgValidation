﻿using System;
using Xunit;

namespace ArgValidation.Tests.StringValidationTests
{
    public partial class ArgumentStringExtensionTest
    {
        [Fact]
        public void NotContains_ArgumentIsNull_ArgumentException()
        {
            string nullValue = null;
            var exc = Assert.Throws<InvalidOperationException>(() => Arg.Validate(() => nullValue).NotContains(""));
            Assert.Equal($"Argument '{nameof(nullValue)}' is null. Сan not execute 'NotContains' method", exc.Message);
        }

        [Fact]
        public void NotContains_ValueNotContainsArgument_Ok()
        {
            string value = "qwe";
            string arg = "123";
            Arg.Validate(() => value).NotContains(arg);
        }

        [Fact]
        public void NotContains_ValueContainsArgument_ArgumentException()
        {
            string value = "string";
            string substring = value.Substring(3);
            ArgumentException exc = Assert.Throws<ArgumentException>(() => Arg.Validate(() => value).NotContains(substring));
            Assert.Equal($"Argument '{nameof(value)}' must not contains '{substring}'. Current value: '{value}'", exc.Message);
        }

        [Fact]
        public void NotContains_ValidationIsDisabled_WithoutException()
        {
            string value = "asdf";
            var arg = new Argument<string>(value, "name", validationIsDisabled: true);
            arg.NotContains(value);
        }
    }
}