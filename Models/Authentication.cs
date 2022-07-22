using System.Linq;
using System.Web;
using userDemo1.Context;

namespace userDemo1.Models
{
    public class Authentication
    {
        public static void Logout()
        {
            HttpContext.Current.Session["signInManager"] = null;
        }

        public static bool Login(UserLogin userLogin)
        {
            dbtestEntities _dbContext = new dbtestEntities();
            var authenticatedUser = _dbContext.Users.Include("Roles").Where(u => u.Email == userLogin.UserId && u.Password == userLogin.Password).FirstOrDefault();

            if (authenticatedUser != null)
            {
                Authentication.SetSignedInManager(authenticatedUser);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void SetSignedInManager(User authenticatedUser)
        {
            dbtestEntities _dbContext = new dbtestEntities();
            var roles = authenticatedUser.Roles.Select(x => x.RoleId);
            var permissions = _dbContext.RolesPermissions.Where(p => roles.Contains(p.RoleId))
                                .Select(x => new Permission
                                {
                                    ModuleName = x.ModuleName,
                                    ViewPermission = x.ViewPermission,
                                    AddPermission = x.AddPermission,
                                    EditPermission = x.EditPermission,
                                    DeletePermission = x.DeletePermission
                                }).ToList();
            SignedInManager signInManager = new SignedInManager()
            {
                User = authenticatedUser,
                Permissions = permissions
            };

            HttpContext.Current.Session["signInManager"] = null;
            HttpContext.Current.Session["signInManager"] = signInManager;
        }

        public static bool IsUserLoggedIn()
        {
            if (HttpContext.Current.Session["signInManager"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}