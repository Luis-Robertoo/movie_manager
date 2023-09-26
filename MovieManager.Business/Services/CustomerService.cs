using FluentResults;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Exceptions;
using MovieManager.BLL.Extensions;
using MovieManager.BLL.Interfaces;

namespace MovieManager.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository CustomerRepository { get; private set; }

        public CustomerService(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        public Result Add(Customer item)
        {
            try
            {
                var result = CustomerRepository.Add(item);

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

        public Result<Customer> FindById(int id)
        {
            try
            {
                var result = CustomerRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Cliente não encontrado");
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

        public Result<List<Customer>> List()
        {
            try
            {
                var result = CustomerRepository.List();

                return result.IsSuccess ? result.Value : new List<Customer>();
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
                var moviment = CustomerRepository.GetById(id);
                if (moviment.Value == null || moviment.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Cliente não encontrado");
                }

                var result = CustomerRepository.Remove(id);

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

        public Result Update(int id, Customer newItem)
        {
            try
            {
                var result = CustomerRepository.GetById(id);
                if (result.Value == null || result.Value == default)
                {
                    return ExceptionExtension.GetNotFoundError("Cliente não encontrado");
                }

                CustomerRepository.Update(id, newItem);

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
