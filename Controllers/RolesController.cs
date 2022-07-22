using System.Linq;
using System.Web.Mvc;
using userDemo1.Context;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class RolesController : Controller
    {
        private dbtestEntities _dbContext = new dbtestEntities();

        public ActionResult Index()
        {
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Roles");
                TempData["userRights"] = userRights;
                var rolesList = _dbContext.Roles.ToList();
                return View(rolesList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Create(Role editModel)
        {
            if (Authentication.IsUserLoggedIn())
            {
                return View(editModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddUpdateRole(Role model)
        {
            if (ModelState.IsValid)
            {
                var userRights = Authorization.GetAuthorizedRights("Roles");
                Role roleRecord = new Role();

                roleRecord.RoleId = model.RoleId;
                roleRecord.RoleName = model.RoleName;
                roleRecord.Active = model.Active;

                if (model.RoleId == 0)
                {
                    if (userRights.AddAuthorized)
                    {
                        _dbContext.Roles.Add(roleRecord);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    if (userRights.EditAuthorized)
                    {
                        _dbContext.Entry(roleRecord).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
            }
            ModelState.Clear();
            var rolesList = _dbContext.Roles.ToList();
            return RedirectToAction("Index", rolesList);
        }

        public ActionResult Delete(int RoleId)
        {
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Roles");
                if (userRights.DeleteAuthorized)
                {
                    var roleRecord = _dbContext.Roles.Where(x => x.RoleId == RoleId).FirstOrDefault();
                    if (roleRecord != null)
                    {
                        _dbContext.Roles.Remove(roleRecord);
                        _dbContext.SaveChanges();
                    }
                    var rolesList = _dbContext.Roles.ToList();
                    return RedirectToAction("Index", rolesList);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}