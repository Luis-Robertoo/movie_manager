using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Exceptions;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;

namespace MovieManager.BLL.Services
{
    public class RentalService : IRentalService
    {
        public IRentalRepository RentalRepository { get; private set; }
        public IMovieRepository MovieRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }

        public RentalService(IRentalRepository rentalRepository,
            IMovieRepository movieRepository, ICustomerRepository customerRepository)
        {
            RentalRepository = rentalRepository;
            MovieRepository = movieRepository;
            CustomerRepository = customerRepository;
        }

        public Result Add(Rental item)
        {
            try
            {
                item.Movie = MovieRepository.GetById(item.MovieId).Value;
                item.Customer = CustomerRepository.GetById(item.CustomerId).Value;

                var result = RentalRepository.Add(item);

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

        public Result<Rental> FindById(int id)
        {
            try
            {
                var result = RentalRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Locação não encontrado");
                }

                result.Value.Movie = MovieRepository.GetById(result.Value.MovieId).Value;
                result.Value.Customer = CustomerRepository.GetById(result.Value.CustomerId).Value;


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

        public Result<List<Rental>> List()
        {
            try
            {
                var result = RentalRepository.List();

                if (result.IsSuccess)
                {
                    foreach (var item in result.Value)
                    {
                        item.Movie = MovieRepository.GetById(item.MovieId).Value;
                        item.Customer = CustomerRepository.GetById(item.CustomerId).Value;
                    }
                }

                return result.IsSuccess ? result.Value : new List<Rental>();
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
                var moviment = RentalRepository.GetById(id);
                if (moviment.Value == null || moviment.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Locação não encontrado");
                }

                var result = RentalRepository.Remove(id);

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

        public Result Update(int id, Rental newItem)
        {
            try
            {
                var result = RentalRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Locação não encontrado");
                }

                RentalRepository.Update(id, newItem);

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

        public Result<List<Movie>> ListMovies()
        {
            try
            {
                return MovieRepository.List();
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

        public Result<List<Customer>> ListCustomers()
        {

            try
            {
                return CustomerRepository.List();
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
