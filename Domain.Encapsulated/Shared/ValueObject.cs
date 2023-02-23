namespace Domain.Encapsulated.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public bool Equals(T other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            return EqualsCore(other);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj is not T other)
            {
                return false;
            }

            return EqualsCore(other);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = default;
            foreach (var component in GetEqualityComponents())
            {
                hashCode.Add(component);
            }

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            var components = GetEqualityComponents().ToList();
            if (components.Count == 1)
            {
                return components.First()?.ToString() ?? string.Empty;
            }

            return $"{{ {string.Join(", ", components)} }}";
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.EqualsCore(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b) => !(a == b);

        protected abstract IEnumerable<object> GetEqualityComponents();

        private bool EqualsCore(ValueObject<T> other)
        {
            if (GetType() != other.GetType())
            {
                return false;
            }

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }
    }
}
