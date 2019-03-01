using System;

namespace Stats.Api.Business.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string message)
            : base(message)
        {
        }

        public ItemNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
