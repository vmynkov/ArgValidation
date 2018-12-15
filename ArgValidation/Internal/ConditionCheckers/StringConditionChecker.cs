﻿using System.Text.RegularExpressions;

namespace ArgValidation.Internal.ConditionCheckers
{
    internal static class StringConditionChecker
    {
        public static bool Match(Argument<string> arg, string pattern)
        {
            Regex r = new Regex(pattern);
            return r.IsMatch(arg.Value);
        }
    }
}