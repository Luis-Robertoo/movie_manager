using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Interfaces;

namespace MovieManager.App.Controllers
{
    public class CustomerController : AbstractController
    {
        public ICustomerService CustomerService { get; private set; }

        public CustomerController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        public IActionResult Index()
        {
            var result = CustomerService.List();

            return View(result.Value);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            var result = CustomerService.Add(customer);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var result = CustomerService.FindById(id);

            return result.IsFailed ? RedirectToAction(nameof(Index)) : View(result.Value);
        }

        public IActionResult Edit(int id)
        {
            var result = CustomerService.FindById(id);

            return result.IsFailed ? RedirectToAction(nameof(Index)) : View(result.Value);
        }

        [HttpPost]
        public IActionResult Edit(int id, Customer customer)
        {
            var result = CustomerService.Update(id, customer);

            return result.IsSuccess ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var result = CustomerService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult List()
        {
            var result = CustomerService.List();

            return result.IsSuccess ? View() : GetErrorResult(result.ToResult());
        }
    }
}
