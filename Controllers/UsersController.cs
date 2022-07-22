using System.Linq;
using System.Web.Mvc;
using userDemo1.Context;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class UsersController : Controller
    {
        private dbtestEntities _dbContext = new dbtestEntities();

        public ActionResult Index()
        {
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Users");
                TempData["userRights"] = userRights;
                var usrList = _dbContext.Users.ToList();
                return View(usrList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Create(User editModel)
        {
            if (Authentication.IsUserLoggedIn())
            {
                if (editModel.UserId != 0)
                {
                    editModel = _dbContext.Users.Where(u => u.UserId == editModel.UserId).FirstOrDefault();
                }
                return View(editModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddUpdateUser(User model)
        {
            if (ModelState.IsValid)
            {
                var userRights = Authorization.GetAuthorizedRights("Users");
                User userRecord = new User();

                userRecord.UserId = model.UserId;
                userRecord.FirstName = model.FirstName;
                userRecord.LastName = model.LastName;
                userRecord.Phone = model.Phone;
                userRecord.Email = model.Email;
                userRecord.Password = model.Password;
                userRecord.Status = model.Status;

                if (model.UserId == 0)
                {
                    if (userRights.AddAuthorized)
                    {
                        _dbContext.Users.Add(userRecord);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    if (userRights.EditAuthorized)
                    {
                        _dbContext.Entry(userRecord).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
            }
            ModelState.Clear();
            var userList = _dbContext.Users.ToList();
            return RedirectToAction("Index", userList);
        }

        public ActionResult Delete(int UserId)
        {
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Users");
                if (userRights.DeleteAuthorized)
                {
                    var userRecord = _dbContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                    if (userRecord != null)
                    {
                        _dbContext.Users.Remove(userRecord);
                        _dbContext.SaveChanges();
                    }
                    var userList = _dbContext.Users.ToList();
                    return RedirectToAction("Index", userList);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult RemoveRole(int UserId, Role role)
        {
            var user = _dbContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            return RedirectToAction("Create", user);
        }

        public ActionResult AddRole(int UserId, Role role)
        {
            Role roleFromDb = _dbContext.Roles.FirstOrDefault(x => x.RoleId == role.RoleId);
            _dbContext.Users.FirstOrDefault(x => x.UserId == UserId).Roles.Add(roleFromDb);
            _dbContext.SaveChanges();

            var user = _dbContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            return RedirectToAction("Create", user);
        }
    }
}