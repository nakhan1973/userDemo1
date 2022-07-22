using System.Web.Mvc;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Authentication.Logout();
            return View("Index");
        }

        public ActionResult Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                if (Authentication.Login(userLogin))
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "Invalid Login Id or Password!");
                    return View("Index");
                }
            }
            return View();
        }
    }
}