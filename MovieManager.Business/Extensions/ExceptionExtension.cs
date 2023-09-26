using FluentResults;
using MovieManager.BLL.Exceptions;

namespace MovieManager.BLL.Extensions
{
    public static class ExceptionExtension
    {
        public static Result GetNotFoundError(string errorMessage = "objeto não encontrado")
        {
            return Result.Fail(new NotFoundException(errorMessage));
        }

        public static Result GetBadRequestError(string errorMessage = "Favor validar request")
        {
            return Result.Fail(new BadRequestException(errorMessage));
        }
    }
}
