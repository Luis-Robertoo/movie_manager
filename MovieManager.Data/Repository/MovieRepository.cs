using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;
using MovieManager.Data.Contexts;

namespace MovieManager.Data.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public AppDbContext Context { get; private set; }

        public MovieRepository(AppDbContext contex)
        {
            Context = contex;
        }

        public Result Add(Movie entity)
        {
            try
            {
                var existAddreses = Context.Movies.FirstOrDefault(x => x.Name == entity.Name &&
                                                                          x.Director == entity.Director &&
                                                                          x.Duration == entity.Duration &&
                                                                          x.AgeGroup == entity.AgeGroup);

                if (existAddreses != null && existAddreses != default)
                {
                    return ExceptionExtension.GetBadRequestError("Filme já existente");
                }

                Context.Movies.Add(entity);

                int result = Context.SaveChanges();

                return Result.OkIf(result.Equals(Context.SuccededResultNumber), "Erro ao tentar salvar filme");
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public Result<Movie> GetById(int id)
        {
            return Result.Ok<Movie>(Context.Movies.FirstOrDefault(address => address.Id.Equals(id)));
        }

        public Result<List<Movie>> List()
        {
            return Result.Ok<List<Movie>>(Context.Movies.ToList());
        }

        public Result<int> Remove(int id)
        {
            var user = GetById(id);

            Context.Remove(user.Value);
            return Result.Ok(Context.SaveChanges());
        }

        public void Update(int id, Movie entity)
        {
            var movie = GetById(id);

            movie.Value.Name = entity.Name;
            movie.Value.Director = entity.Director;
            movie.Value.MovieKind = entity.MovieKind;
            movie.Value.Description = entity.Description;
            movie.Value.Duration = entity.Duration;
            movie.Value.AgeGroup = entity.AgeGroup;
            movie.Value.RentalValue = entity.RentalValue;

            Context.SaveChanges();
        }
    }
}
