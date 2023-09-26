using FluentResults;
using MovieManager.BLL.Entities;

namespace MovieManager.BLL.Interfaces
{
    public interface IRentalService : IBaseService<Rental>
    {
        Result<List<Movie>> ListMovies();
        Result<List<Customer>> ListCustomers();
    }
}
