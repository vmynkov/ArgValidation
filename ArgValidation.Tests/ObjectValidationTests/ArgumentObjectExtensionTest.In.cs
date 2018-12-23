﻿using System;
using Xunit;

namespace ArgValidation.Tests.ObjectValidationTests
{
    public partial class ArgumentObjectExtensionTest
    {
        [Fact]
        public void In_ListIsNull_InvalidOperationException()
        {
            var value = new object();
            object[] nullArgument = null;

            InvalidOperationException exc = Assert.Throws<InvalidOperationException>(() => Arg.Validate(() => value).In(nullArgument));

            Assert.Equal("Argument 'values' is null. There are no values to compare", exc.Message);
        }

        [Fact]
        public void In_ArgumentIsNullAndListContainsOnlyNull_Ok()
        {
            object nullArg = null;
            var listContainedOnlyNull = new object[] { null };

            Arg.Validate(() => nullArg).In(listContainedOnlyNull);
        }

        [Fact]
        public void In_ArgumentContainsInList_Ok()
        {
            Arg.Validate(() => 3).In(3, 4);
        }

        [Fact]
        public void In_ArgumentNotContainsInList_ArgumentException()
        {
            int value3 = 3;
            ArgumentException exc = Assert.Throws<ArgumentException>(() => Arg.Validate(() => value3).In(2, 1));
            Assert.Equal($"Argument '{nameof(value3)}' can only have the following values: '2', '1'. Current value: '3'", exc.Message);
        }

        [Fact]
        public void In_ValidationIsDisabled_WithoutException()
        {
            int value = 1;
            var arg = new Argument<int>(value, "name", validationIsDisabled: true);
            arg.In(2, 3);
        }
    }
}