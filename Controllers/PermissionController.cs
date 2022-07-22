using System.Linq;
using System.Web.Mvc;
using userDemo1.Context;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class PermissionController : Controller
    {
        private dbtestEntities _dbContext = new dbtestEntities();

        // GET: Permission
        public ActionResult Index(int RoleId)
        {
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Permissions");
                TempData["userRights"] = userRights;
                var roleWithPermissions = GetRoleInformation(RoleId);
                return View(roleWithPermissions);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private Role GetRoleInformation(int RoleId)
        {
            var roleWithPermissions = _dbContext.Roles.Where(x => x.RoleId == RoleId).FirstOrDefault();
            return roleWithPermissions;
        }

        public ActionResult Create(RolesPermission editModel, int RoleId)
        {
            if (Authentication.IsUserLoggedIn())
            {
                if (editModel == null)
                {
                    editModel = new RolesPermission();
                    editModel.RoleId = RoleId;
                }
                return View(editModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddUpdatePermission(RolesPermission model)
        {
            if (ModelState.IsValid)
            {
                RolesPermission RolePermission = new RolesPermission();

                RolePermission.Id = model.Id;
                RolePermission.ModuleName = model.ModuleName;
                RolePermission.ViewPermission = model.ViewPermission;
                RolePermission.AddPermission = model.AddPermission;
                RolePermission.EditPermission = model.EditPermission;
                RolePermission.DeletePermission = model.DeletePermission;
                RolePermission.RoleId = model.RoleId;

                if (model.Id == 0)
                {
                    var userRights = Authorization.GetAuthorizedRights("Permissions");
                    if (userRights.AddAuthorized)
                    {
                        _dbContext.RolesPermissions.Add(RolePermission);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    var userRights = Authorization.GetAuthorizedRights("Permissions");
                    if (userRights.EditAuthorized)
                    {
                        _dbContext.Entry(RolePermission).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
            }
            ModelState.Clear();
            Role role = GetRoleInformation(model.RoleId);
            return RedirectToAction("Index", role);
        }

        public ActionResult Delete(int Id)
        {
            var rolePermission = _dbContext.RolesPermissions.Where(x => x.Id == Id).FirstOrDefault();
            if (rolePermission != null)
            {
                var userRights = Authorization.GetAuthorizedRights("Permissions");
                if (userRights.DeleteAuthorized)
                {
                    _dbContext.RolesPermissions.Remove(rolePermission);
                    _dbContext.SaveChanges();
                }
            }
            Role role = GetRoleInformation(rolePermission.RoleId);
            return RedirectToAction("Index", role);
        }
    }
}