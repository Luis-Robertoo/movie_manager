using FluentResults;

namespace MovieManager.BLL.Exceptions
{
    public class BadRequestException : Error
    {
        public BadRequestException(string message) : base(message) { }
    }
}
