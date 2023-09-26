using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Exceptions;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;

namespace MovieManager.BLL.Services
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; private set; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public Result Add(User item)
        {
            try
            {
                var result = UserRepository.Add(item);

                return result;
            }
            catch (ModelException exception)
            {
                return Result.Fail(new BadRequestException(exception.Message));
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public Result Update(int id, User newItem)
        {
            try
            {
                var result = UserRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Usuario não encontrado");
                }

                UserRepository.Update(id, newItem);

                return Result.Ok();
            }
            catch (ModelException exception)
            {
                return Result.Fail(new BadRequestException(exception.Message));
            }
            catch (Exception exception)
            {
                return Result.Fail(new InternalServerError(exception.Message));
            }
        }

        public Result Remove(int id)
        {
            try
            {
                var moviment = UserRepository.GetById(id);
                if (moviment.Value == null || moviment.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Usuario não encontrado");
                }

                var result = UserRepository.Remove(id);

                return Result.Ok();
            }
            catch (ModelException exception)
            {
                return Result.Fail(new BadRequestException(exception.Message));
            }
            catch (Exception exception)
            {
                return Result.Fail(new InternalServerError(exception.Message));
            }
        }

        public Result<User> FindById(int id)
        {
            try
            {
                var result = UserRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Usuario não encontrado");
                }

                return result;
            }
            catch (ModelException exception)
            {
                return Result.Fail(new BadRequestException(exception.Message));
            }
            catch (Exception exception)
            {
                return Result.Fail(new InternalServerError(exception.Message));
            }
        }

        public Result<List<User>> List()
        {
            try
            {
                var result = UserRepository.List();

                return result.IsSuccess ? result.Value : new List<User>();
            }
            catch (ModelException exception)
            {
                return Result.Fail(new BadRequestException(exception.Message));
            }
            catch (Exception exception)
            {
                return Result.Fail(new InternalServerError(exception.Message));
            }
        }
    }
}
