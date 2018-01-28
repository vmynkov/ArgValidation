﻿using System;

namespace Mynkovv.Validating
{
    public abstract class ValidatorBase<TValue, TInheritInstance>
    {
        private ValidatingObject<TValue> ValidatingObject { get; }

        protected abstract TInheritInstance CreateInstance();

        internal ValidatorBase(ValidatingObject<TValue> validatingObject)
        {
            ValidatingObject = validatingObject ?? throw new ArgumentNullException(nameof(validatingObject));
        }

        public TInheritInstance Default()
        {
            if (!ConditionChecker.IsDefault(ValidatingObject.Value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be default value. Current value: '{ValidatingObject.Value}'");

            return CreateInstance();
        }

        public TInheritInstance NotDefault()
        {
            if (ConditionChecker.IsDefault(ValidatingObject.Value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be not default value");

            return CreateInstance();
        }

        public TInheritInstance Null()
        {
            if (!ConditionChecker.IsNull(ValidatingObject.Value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be null. Current value: '{ValidatingObject.Value}'");

            return CreateInstance();
        }

        public TInheritInstance NotNull()
        {
            if (ConditionChecker.IsNull(ValidatingObject.Value))
                throw new ArgumentNullException(ValidatingObject.Name);

            return CreateInstance();
        }

        public TInheritInstance Equal(TValue value)
        {
            if (!ConditionChecker.IsEqual(ValidatingObject.Value, value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be equal '{value}'. Current value: '{ValidatingObject.Value}'");

            return CreateInstance();
        }

        public TInheritInstance NotEqual(TValue value)
        {
            if (ConditionChecker.IsEqual(ValidatingObject.Value, value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be not equal '{value}'");

            return CreateInstance();
        }

        public TInheritInstance MoreThan(TValue value)
        {
            if (!ConditionChecker.MoreThan(ValidatingObject, value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be more than '{value}'");

            return CreateInstance();
        }

        public TInheritInstance MoreOrEqualThan(TValue value)
        {
            if (!ConditionChecker.MoreOrEqualThan(ValidatingObject, value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be more or equal than '{value}'. Current value: '{ValidatingObject.Value}'");

            return CreateInstance();
        }

        public TInheritInstance LessThan(TValue value)
        {
            if (!ConditionChecker.LessThan(ValidatingObject, value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be less than '{value}'. Current value: '{ValidatingObject.Value}'");

            return CreateInstance();
        }

        public TInheritInstance LessOrEqualThan(TValue value)
        {
            if (!ConditionChecker.LessOrEqualThan(ValidatingObject, value))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be less or equal than '{value}'. Current value: '{ValidatingObject.Value}'");

            return CreateInstance();
        }

        public TInheritInstance InRange(TValue min, TValue max)
        {
            if (!ConditionChecker.InRange(ValidatingObject, min, max))
                throw new ArgumentException($"Object with name '{ValidatingObject.Name}' must be in range from '{min}' to '{max}'. Current value: '{ValidatingObject.Value}'");

            return CreateInstance();
        }

        public TInheritInstance OnlyValues(params TValue[] values)
        {
            throw new NotImplementedException();
        }
    }
}
