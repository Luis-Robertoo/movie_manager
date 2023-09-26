using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Exceptions;

namespace MovieManager.BLL.Interfaces
{
    public abstract class AbstractController : Controller
    {
        protected IActionResult GetErrorResult(Result result)
        {
            if (result.HasError<NotFoundException>())
            {
                return NotFound(result.Reasons);
            }
            else if (result.HasError<BadRequestException>())
            {
                return BadRequest(result.Errors);
            }

            return StatusCode(500);
        }
    }
}
