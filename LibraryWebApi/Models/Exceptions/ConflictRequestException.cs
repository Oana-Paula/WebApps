namespace LibraryWebApi.Models.Exceptions
{
    public class ConflictException : System.Exception
    {
        public ConflictException()
        { }

        public ConflictException(string message) : base(message)
        {
        }
    }
}