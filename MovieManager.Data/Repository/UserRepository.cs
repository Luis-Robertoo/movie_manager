using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;
using MovieManager.Data.Contexts;

namespace MovieManager.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public AppDbContext Context { get; private set; }

        public UserRepository(AppDbContext contex)
        {
            Context = contex;
        }

        public Result Add(User entity)
        {
            try
            {
                var existAddreses = Context.Users.FirstOrDefault(x => x.Name == entity.Name &&
                                                                    x.Age == entity.Age);

                if (existAddreses != null && existAddreses != default)
                {
                    return ExceptionExtension.GetBadRequestError("Usuario já existente");
                }

                Context.Users.Add(entity);

                int result = Context.SaveChanges();

                return Result.OkIf(result.Equals(Context.SuccededResultNumber), "Erro ao tentar salvar usuario");
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public Result<User> GetById(int id)
        {
            return Result.Ok<User>(Context.Users.FirstOrDefault(address => address.Id.Equals(id)));
        }

        public Result<List<User>> List()
        {
            return Result.Ok<List<User>>(Context.Users.ToList());
        }

        public void Update(int id, User entity)
        {
            var user = GetById(id);

            user.Value.Name = entity.Name;
            user.Value.Age = entity.Age;
            user.Value.Email = entity.Email;
            user.Value.Phone = entity.Phone;

            Context.SaveChanges();
        }

        public Result<int> Remove(int id)
        {
            var user = GetById(id);

            Context.Remove(user.Value);
            return Result.Ok(Context.SaveChanges());
        }
    }
}
