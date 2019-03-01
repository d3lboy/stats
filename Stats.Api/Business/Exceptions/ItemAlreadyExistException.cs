using System;

namespace Stats.Api.Business.Exceptions
{
    public class ItemAlreadyExistException : Exception
    {
        public ItemAlreadyExistException()
        {
        }

        public ItemAlreadyExistException(string message)
            : base(message)
        {
        }

        public ItemAlreadyExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
