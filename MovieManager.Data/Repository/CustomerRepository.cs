using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;
using MovieManager.Data.Contexts;

namespace MovieManager.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public AppDbContext Context { get; private set; }

        public CustomerRepository(AppDbContext contex)
        {
            Context = contex;
        }

        public Result Add(Customer entity)
        {
            try
            {
                var actualCustomer = Context.Customers.FirstOrDefault(x => x.Name == entity.Name &&
                                                                            x.Age == entity.Age &&
                                                                            x.Email == entity.Email &&
                                                                            x.Phone == entity.Phone);

                if (actualCustomer != null && actualCustomer != default)
                {
                    return ExceptionExtension.GetBadRequestError("Cliente já existente");
                }

                Context.Customers.Add(entity);

                int result = Context.SaveChanges();

                return Result.OkIf(result.Equals(Context.SuccededResultNumber), "Erro ao tentar salvar cliente");
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public Result<Customer> GetById(int id)
        {
            return Result.Ok<Customer>(Context.Customers.FirstOrDefault(customer => customer.Id.Equals(id)));
        }

        public Result<List<Customer>> List()
        {
            return Result.Ok<List<Customer>>(Context.Customers.ToList());
        }

        public Result<int> Remove(int id)
        {
            var user = GetById(id);

            Context.Remove(user.Value);

            return Result.Ok(Context.SaveChanges());
        }

        public void Update(int id, Customer entity)
        {
            var user = GetById(id);

            user.Value.Name = entity.Name;
            user.Value.Age = entity.Age;
            user.Value.Email = entity.Email;
            user.Value.Phone = entity.Phone;
            user.Value.Rental = entity.Rental;

            Context.SaveChanges();
        }
    }
}
