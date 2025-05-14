using Microsoft.AspNetCore.Mvc;

namespace ProductCommentApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
