namespace Domain.Encapsulated.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DomainException : Exception
    {
        public DomainException()
            : base()
        {
        }

        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
