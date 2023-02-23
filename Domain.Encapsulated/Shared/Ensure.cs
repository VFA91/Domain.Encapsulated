namespace Domain.Encapsulated.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public static class Ensure
    {
        public static void That(bool condition, string message = "")
        {
            if (!condition)
            {
                throw new DomainException(message);
            }
        }

        public static void That<TException>(bool condition, string message = "")
            where TException : Exception
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }

        public static void That<TException>(bool condition, params object[] args)
            where TException : Exception
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), args);
            }
        }

        public static void Not(bool condition, string message = "") => That(!condition, message);

        public static void Not<TException>(bool condition, string message = "")
            where TException : Exception => That<TException>(!condition, message);

        public static void EntityExists(object value, string message) => That(value is not null, message);

        public static void NotNullOrEmpty(string value, string message) => That(!string.IsNullOrEmpty(value), message);

        public static void Equal<T>(T left, T right, string message = "Values must be equal") => That(left is not null && right is not null && left.Equals(right), message);

        public static void NotEqual<T>(T left, T right, string message = "Values must not be equal") => That(left is not null && right is not null && !left.Equals(right), message);

        public static void Contains<T>(IEnumerable<T> collection, Func<T, bool> predicate, string message = "") => That(collection is not null && collection.Any(predicate), message);

        public static void Items<T>(IEnumerable<T> collection, Func<T, bool> predicate, string message = "") => That(collection is not null && collection.All(predicate), message);

        public static class Argument
        {
            public static void Is(bool condition, string message = "") => That<ArgumentException>(condition, message);

            public static void IsNot(bool condition, string message = "") => Is(!condition, message);

            public static void NotNull(object value, string paramName = "") => That<ArgumentNullException>(value != null, paramName);

            public static void NotNullOrWhiteSpace(string value, string paramName = "")
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(Validations.Resources.StringNotNullOrWhitespace, paramName));
                }
            }
        }
    }
}
