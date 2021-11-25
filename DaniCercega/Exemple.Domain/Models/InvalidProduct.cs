using System;
using System.Runtime.Serialization;

namespace Exemple.Domain.Models
{
    [Serializable]
    internal class InvalidProduct : Exception
    {
        public InvalidProduct()
        {
        }

        public InvalidProduct(string? message) : base(message)
        {
        }

        public InvalidProduct(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidProduct(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}