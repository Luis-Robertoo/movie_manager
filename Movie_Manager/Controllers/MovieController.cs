using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Entities;
using MovieManager.BLL.Interfaces;

namespace MovieManager.App.Controllers
{
    public class MovieController : AbstractController
    {
        public IMovieService MovieService { get; private set; }

        public MovieController(IMovieService movieService)
        {
            MovieService = movieService;
        }

        public IActionResult Index()
        {
            var result = MovieService.List();

            return View(result.Value);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var result = MovieService.FindById(id);

            return result.IsFailed ? RedirectToAction(nameof(Index)) : View(result.Value);
        }

        public IActionResult Edit(int id)
        {
            var result = MovieService.FindById(id);

            return result.IsFailed ? RedirectToAction(nameof(Index)) : View(result.Value);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            var result = MovieService.Add(movie);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(int id, Movie movie)
        {
            var result = MovieService.Update(id, movie);

            return result.IsSuccess ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var result = MovieService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult List()
        {
            var result = MovieService.List();

            return result.IsSuccess ? View() : GetErrorResult(result.ToResult());
        }
    }
}
