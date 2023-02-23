namespace Domain.Encapsulated.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ComparableValueObject<T> : ValueObject<T>, IComparable, IComparable<T>
        where T : ComparableValueObject<T>
    {
        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is not T other)
            {
                throw new ArgumentException("Object is not of a comparable type.");
            }

            return CompareToCore(other);
        }

        public int CompareTo(T other)
        {
            if (other is null)
            {
                return 1;
            }

            return CompareToCore(other);
        }

        public static bool operator <(ComparableValueObject<T> left, ComparableValueObject<T> right)
        {
            if (right is null)
            {
                return false;
            }

            if (left is null)
            {
                return true;
            }

            return left.CompareToCore(right) < 0;
        }

        public static bool operator >(ComparableValueObject<T> left, ComparableValueObject<T> right)
        {
            if (left is null)
            {
                return false;
            }

            if (right is null)
            {
                return true;
            }

            return left.CompareToCore(right) > 0;
        }

        public static bool operator <=(ComparableValueObject<T> left, ComparableValueObject<T> right) => !(left > right);

        public static bool operator >=(ComparableValueObject<T> left, ComparableValueObject<T> right) => !(left < right);

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(T))
            {
                return false;
            }

            return Equals((T)obj);
        }

        public override int GetHashCode() => base.GetHashCode();

        protected abstract IEnumerable<IComparable> GetComparisonComponents();

        protected override IEnumerable<object> GetEqualityComponents() => GetComparisonComponents();

        private int CompareToCore(ComparableValueObject<T> other)
        {
            var thisComponents = GetComparisonComponents().ToArray();
            var otherComponents = other.GetComparisonComponents().ToArray();

            var comparison = 0;
            for (var i = 0; comparison == 0 && i < thisComponents.Length && i < otherComponents.Length; i++)
            {
                comparison = Compare(thisComponents[i], otherComponents[i]);
            }

            if (comparison == 0)
            {
                comparison = Compare(thisComponents.Length, otherComponents.Length);
            }

            return comparison;
        }

        private static int Compare(IComparable left, IComparable right)
        {
            if (left is null && right is null)
            {
                return 0;
            }

            if (left is null)
            {
                return -1;
            }

            if (right is null)
            {
                return 1;
            }

            return left.CompareTo(right);
        }
    }
}
