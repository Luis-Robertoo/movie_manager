using FluentResults;

namespace MovieManager.BLL.Exceptions
{
    public class InternalServerError : Error
    {
        public InternalServerError(string message) : base(message) { }
    }
}
