using FluentResults;

namespace MovieManager.BLL.Exceptions
{
    public class NotFoundException : Error
    {
        public NotFoundException(string message) : base(message) { }
    }
}
