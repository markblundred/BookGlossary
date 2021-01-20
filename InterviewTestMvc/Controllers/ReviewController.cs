using InterviewTestMvc.DataServices;
using InterviewTestMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace InterviewTestMvc.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IConfiguration configuration;

        public ReviewController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public IActionResult Index(long bookId)
        {
            var reviewDataService = new ReviewDataService(configuration.GetConnectionString("SavaBookTask"));
            var reviews = reviewDataService.GetReviews(bookId);
            return View(reviews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}