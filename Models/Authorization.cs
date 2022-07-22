using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using userDemo1.Context;

namespace userDemo1.Models
{
    public class Authorization
    {
        public static UserRights GetAuthorizedRights(string moduleName)
        {
            var rights = new UserRights();
            var signInManager = (SignedInManager)HttpContext.Current.Session["signInManager"];
            rights.ViewAuthorized = signInManager.Permissions.Where(x => x.ModuleName.ToLower() == moduleName.ToLower() && x.ViewPermission == true).Any();
            rights.AddAuthorized = signInManager.Permissions.Where(x => x.ModuleName.ToLower() == moduleName.ToLower() && x.AddPermission == true).Any();
            rights.EditAuthorized = signInManager.Permissions.Where(x => x.ModuleName.ToLower() == moduleName.ToLower() && x.EditPermission == true).Any();
            rights.DeleteAuthorized = signInManager.Permissions.Where(x => x.ModuleName.ToLower() == moduleName.ToLower() && x.DeletePermission == true).Any();

            return rights;
        }
        public static SignedInManager GetCurrentUser()
        {
            var signInManager = (SignedInManager)HttpContext.Current.Session["signInManager"];
            return signInManager;
        }

        public static List<Role> GetAllRoles()
        {
            dbtestEntities _dbContext = new dbtestEntities();
            return _dbContext.Roles.ToList();
        }
    }
}