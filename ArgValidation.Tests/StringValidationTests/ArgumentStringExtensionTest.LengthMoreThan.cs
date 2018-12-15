﻿using System;
using Xunit;

namespace ArgValidation.Tests.StringValidationTests
{
    public partial class ArgumentStringExtensionTest
    {
        [Fact]
        public void LengthMoreThan_ArgumentIsNull_InvalidOperationException()
        {
            int length = 2;
            string nullString = null;
            var exc = Assert.Throws<InvalidOperationException>(() => Arg.Validate(() => nullString).LengthMoreThan(length));
            Assert.Equal($"Argument '{nameof(nullString)}' is null. Сan not execute 'LengthMoreThan' method", exc.Message);
        }

        [Fact]
        public void LengthMoreThan_Equals_ArgumentException()
        {
            string str = "str";
            int length = str.Length;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => Arg.Validate(() => str).LengthMoreThan(length));
            Assert.Equal($"Argument '{nameof(str)}' must be length more than {length}. Current length: {str.Length}", exc.Message);
        }

        [Fact]
        public void LengthMoreThan_Less_ArgumentException()
        {
            string str = "str";
            int length = str.Length + 1;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => Arg.Validate(() => str).LengthMoreThan(length));
            Assert.Equal($"Argument '{nameof(str)}' must be length more than {length}. Current length: {str.Length}", exc.Message);
        }

        [Fact]
        public void LengthMoreThan_More_Ok()
        {
            string str = "str";
            int length = str.Length - 1;
            Arg.Validate(() => str).LengthMoreThan(length);
        }

        [Fact]
        public void LengthMoreThan_ValidationIsDisabled_WithoutException()
        {
            string value = "value";
            var arg = new Argument<string>(value, "name", validationIsDisabled: true);
            arg.LengthMoreThan(value.Length + 1);
        }
    }
}