using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Exceptions;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;

namespace MovieManager.BLL.Services
{
    public class MovieService : IMovieService
    {
        public IMovieRepository MovieRepository { get; private set; }

        public MovieService(IMovieRepository movieRepository)
        {
            MovieRepository = movieRepository;
        }

        public Result Add(Movie item)
        {
            try
            {
                var result = MovieRepository.Add(item);

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

        public Result<Movie> FindById(int id)
        {
            try
            {
                var result = MovieRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Filme não encontrado");
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

        public Result<List<Movie>> List()
        {
            try
            {
                var result = MovieRepository.List();

                return result.IsSuccess ? result.Value : new List<Movie>();
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
                var moviment = MovieRepository.GetById(id);
                if (moviment.Value == null || moviment.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Filme não encontrado");
                }

                var result = MovieRepository.Remove(id);

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

        public Result Update(int id, Movie newItem)
        {
            try
            {
                var result = MovieRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Filme não encontrado");
                }

                MovieRepository.Update(id, newItem);

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
    }
}
