using System;
using System.Runtime.Serialization;

namespace Exemple.Domain.Models
{
    [Serializable]
    internal class InvalidOrderIdException : Exception
    {
        public InvalidOrderIdException()
        {
        }

        public InvalidOrderIdException(string? message) : base(message)
        {
        }

        public InvalidOrderIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidOrderIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}