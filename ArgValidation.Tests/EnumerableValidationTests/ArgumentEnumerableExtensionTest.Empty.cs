﻿using System;
using Xunit;

namespace ArgValidation.Tests.EnumerableValidationTests
{
    public partial class ArgumentEnumerableExtensionTest
    {
        [Fact]
        public void Empty_ValidationIsDisabled_WithoutException()
        {
            int[] notEmpty = { 1 };
            var arg = new Argument<int[]>(notEmpty, "name", validationIsDisabled: true);
            arg.Empty();
        }
    }
}