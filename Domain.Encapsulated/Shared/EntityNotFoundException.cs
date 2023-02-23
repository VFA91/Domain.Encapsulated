namespace Domain.Encapsulated.Shared
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(MemberInfo entityType, object entityId)
            : base(FormatMessage(entityType.Name, entityId))
        {
        }

        public EntityNotFoundException(MemberInfo entityType, IDictionary<string, string> values)
            : base(FormatMessage(entityType.Name, values))
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string FormatMessage(string entityType, object entityId) => string.Format(Resources.EntityNotFound, entityType, entityId);
        private static string FormatMessage(string entityType, IDictionary<string, string> values) => string.Format(Resources.EntityNotFound, entityType, JsonConvert.SerializeObject(values));
    }

    [Serializable]
    public class RelatedEntityNotFoundException : Exception
    {
        public RelatedEntityNotFoundException()
        {
        }

        public RelatedEntityNotFoundException(MemberInfo entityType, object entityId)
            : base(FormatMessage(entityType.Name, entityId))
        {
        }

        protected RelatedEntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected RelatedEntityNotFoundException(string message)
            : base(message)
        {
        }

        private static string FormatMessage(string entityType, object entityId) => string.Format(Resources.EntityNotFound, entityType, entityId);
    }

    [Serializable]
    public class InvalidReferenceException : Exception
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public InvalidReferenceException()
        {
        }

        public InvalidReferenceException(MemberInfo entityType, int entityId)
            : base(FormatMessage(entityType.Name, entityId))
        {
            Name = entityType.Name;
            Id = entityId;
        }

        public InvalidReferenceException(MemberInfo entityType, IDictionary<string, string> values)
            : base(FormatMessage(entityType.Name, values))
        {
            Name = entityType.Name;
            Id = default;
        }

        protected InvalidReferenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected InvalidReferenceException(string message)
            : base(message)
        {
        }

        private static string FormatMessage(string entityType, int entityId) => string.Format(Resources.InvalidReference, entityType, entityId);

        private static string FormatMessage(string entityType, IDictionary<string, string> values) => string.Format(Resources.InvalidReferenceWithValues, entityType, JsonConvert.SerializeObject(values));
    }
}