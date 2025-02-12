using Microsoft.AspNetCore.Mvc;

namespace CRUDApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
