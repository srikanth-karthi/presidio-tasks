using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
        
        public IActionResult About()
        {
            return File("~/about.html", "text/html");
        }

        public IActionResult Contact()
        {
            return File("~/contact.html", "text/html");
        }
    }
}
