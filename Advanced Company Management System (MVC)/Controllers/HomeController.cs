using Advanced_Company_Management_System__MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Advanced_Company_Management_System__MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string testttttt()
        {
            return "hellow";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode ?? Response.StatusCode,
                ShowDetails = _environment.IsDevelopment()
            };

            // Try to get exception details if available
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerFeature?.Error != null)
            {
                errorViewModel.ErrorMessage = exceptionHandlerFeature.Error.Message;
                errorViewModel.StackTrace = exceptionHandlerFeature.Error.StackTrace;

                // Log the error
                _logger.LogError(exceptionHandlerFeature.Error,
                    "An unhandled exception occurred. Path: {Path}",
                    exceptionHandlerFeature.Path);
            }

            // Set appropriate status code
            if (statusCode.HasValue)
            {
                Response.StatusCode = statusCode.Value;
            }

            return View(errorViewModel);
        }
    }
}
