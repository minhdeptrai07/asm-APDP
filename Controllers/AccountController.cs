using Microsoft.AspNetCore.Mvc;
using ManagerStudent.Facade;
using Microsoft.AspNetCore.Http;
namespace ManagerStudent.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserFacade _userFacade;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountController(IUserFacade userFacade, IHttpContextAccessor httpContextAccessor)
        {
            _userFacade = userFacade;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string fullName, string email)
        {
            if (_userFacade.RegisterUser(username, password, fullName, email))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Tài khoản đã tồn tại.";
            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userFacade.AuthenticateUser(username, password);
            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("Username", user.Username);
                _httpContextAccessor.HttpContext.Session.SetInt32("RoleId", (int)user.RoleId);
                switch (user.RoleId)
                {
                    case 1: // Admin
                        return RedirectToAction("ManagerCourse", "Manager");
                    case 2: // Faculty
                        return RedirectToAction("Index", "Home");
                    case 3: // Student
                        return RedirectToAction("Index", "Home");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
            return View();
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear(); // Xóa session
            return RedirectToAction("Login");
        }
    }
}

