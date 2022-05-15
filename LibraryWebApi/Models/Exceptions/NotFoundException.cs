using System;

namespace LibraryWebApi.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        { }

        public NotFoundException(string messages) : base(messages)
        {
        }
    }
}