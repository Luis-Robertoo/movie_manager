using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Interfaces;

namespace MovieManager.App.Controllers
{
    public class RentalController : AbstractController
    {
        public IRentalService RentalService { get; private set; }

        public RentalController(IRentalService rentalService)
        {
            RentalService = rentalService;
        }

        public IActionResult Index()
        {
            var result = RentalService.List();

            return View(result.Value);
        }

        public IActionResult Add()
        {
            ViewBag.MovieId = new SelectList(RentalService.ListMovies().Value,"Id", "Name");
            ViewBag.CustomerId = new SelectList(RentalService.ListCustomers().Value,"Id", "Name");

            return View();
        }

        public IActionResult Details(int id)
        {
            var result = RentalService.FindById(id);

            return result.IsFailed ? RedirectToAction(nameof(Index)) : View(result.Value);
        }

        public IActionResult Edit(int id)
        {
            var result = RentalService.FindById(id);

            ViewBag.MovieId = new SelectList(RentalService.ListMovies().Value, "Id", "Name",result.Value.MovieId);
            ViewBag.CustomerId = new SelectList(RentalService.ListCustomers().Value, "Id", "Name", result.Value.CustomerId);

            return result.IsFailed ? RedirectToAction(nameof(Index)) : View(result.Value);
        }

        [HttpPost]
        public IActionResult Create(Rental rental)
        {
            var result = RentalService.Add(rental);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(int id, Rental rental)
        {
            var result = RentalService.Update(id, rental);

            return result.IsSuccess ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var result = RentalService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult List()
        {
            var result = RentalService.List();

            return result.IsSuccess ? View() : GetErrorResult(result.ToResult());
        }
    }
}
