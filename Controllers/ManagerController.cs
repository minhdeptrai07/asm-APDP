using Microsoft.AspNetCore.Mvc;

namespace ManagerStudent.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
