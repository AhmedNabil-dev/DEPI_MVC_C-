using Assisgnment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Assisgnment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ServerError()
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return View("InternalServerError");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public new IActionResult StatusCode(int code)
        {
            if (code == StatusCodes.Status404NotFound)
            {
                Response.StatusCode = code;
                return View("NotFound");
            }

            if (code >= StatusCodes.Status500InternalServerError)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return View("InternalServerError");
            }

            return View("NotFound");
        }
    }
}
