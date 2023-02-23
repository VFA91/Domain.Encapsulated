namespace System.Text
{
    public static class StringBuilderExtension
    {
        public static StringBuilder AppendIf(this StringBuilder @this, bool condition, string value)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            if (condition)
            {
                @this.AppendLine(value);
            }

            return @this;
        }

        public static StringBuilder AppendIfElse(
            this StringBuilder @this,
            bool condition,
            Func<StringBuilder, StringBuilder> contentOfIf,
            Func<StringBuilder, StringBuilder> contentOfElse)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            if (condition)
            {
                contentOfIf(@this);
            }
            else
            {
                contentOfElse(@this);
            }

            return @this;
        }
    }
}
