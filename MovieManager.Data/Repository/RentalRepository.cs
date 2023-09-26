using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;
using MovieManager.Data.Contexts;

namespace MovieManager.Data.Repository
{
    public class RentalRepository : IRentalRepository
    {
        public AppDbContext Context { get; private set; }

        public RentalRepository(AppDbContext contex)
        {
            Context = contex;
        }

        public Result Add(Rental entity)
        {
            try
            {
                var existAddreses = Context.Rentals.FirstOrDefault(x => x.MovieId == entity.MovieId &&
                                                                    x.CustomerId == entity.CustomerId &&
                                                                    x.RentalDateStart == entity.RentalDateStart);

                if (existAddreses != null && existAddreses != default)
                {
                    return ExceptionExtension.GetBadRequestError("Aluguel já existente");
                }

                Context.Rentals.Add(entity);

                int result = Context.SaveChanges();

                return Result.OkIf(result.Equals(Context.SuccededResultNumber), "Erro ao tentar salvar aluguel");
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public Result<Rental> GetById(int id)
        {
            return Result.Ok<Rental>(Context.Rentals.FirstOrDefault(address => address.Id.Equals(id)));
        }

        public Result<List<Rental>> List()
        {
            return Result.Ok<List<Rental>>(Context.Rentals.ToList());
        }

        public Result<int> Remove(int id)
        {
            var user = GetById(id);

            Context.Remove(user.Value);
            return Result.Ok(Context.SaveChanges());
        }

        public void Update(int id, Rental entity)
        {
            var rental = GetById(id);

            rental.Value.RentalDateStart = entity.RentalDateStart;
            rental.Value.ReturnDate = entity.ReturnDate;
            rental.Value.RentalForeseenDateEnd = entity.RentalForeseenDateEnd;
            rental.Value.CustomerId = entity.CustomerId;
            rental.Value.MovieId = entity.MovieId;

            Context.SaveChanges();
        }
    }
}
