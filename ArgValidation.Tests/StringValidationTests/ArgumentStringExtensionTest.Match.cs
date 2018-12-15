﻿using System;
using Xunit;

namespace ArgValidation.Tests.StringValidationTests
{
    public partial class ArgumentStringExtensionTest
    {
        [Fact]
        public void Match_IsMatch_Ok()
        {
            string digits = "1234567890";
            Arg.Validate(digits, nameof(digits))
                .Match("\\d{10}");
        }

        [Fact]
        public void Match_IsNotMatch_ArgumentException()
        {
            string letters = "asdf";
            const string pattern = "\\d{10}";
            ArgumentException exc = Assert.Throws<ArgumentException>(() => Arg.Validate(letters, nameof(letters)).Match(pattern));
            Assert.Equal($"Argument '{nameof(letters)}' must be match with pattern '{pattern}'. Current value: '{letters}'",exc.Message);
        }

        [Fact]
        public void Match_ArgumentValueIsNull_InvalidOperationException()
        {
            string nullValue = null;
            const string pattern = "\\d{10}";
            InvalidOperationException exc = Assert.Throws<InvalidOperationException>(() => Arg.Validate(nullValue, nameof(nullValue)).Match(pattern));
            Assert.Equal($"Argument '{nameof(nullValue)}' is null. Сan not execute 'Match' method", exc.Message);
        }

        [Fact]
        public void Match_Pattern_InvalidOperationException()
        {
            string argValue = "some-value";
            InvalidOperationException exc = Assert.Throws<InvalidOperationException>(() => 
                Arg.Validate(argValue, nameof(argValue))
                    .Match(pattern: null));
            Assert.Equal($"Argument 'pattern' of method 'Match' is null. Can not execute 'Match' method", exc.Message);
        }

        [Fact]
        public void Match_ValidationIsDisabled_WithoutException()
        {
            string letters = "asdf";
            var arg = new Argument<string>(letters, "name", validationIsDisabled: true);
            arg.Match("\\d{10}");
        }
    }
}