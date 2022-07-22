using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class CustomerController : Controller
    {
        private static List<Customer> customerList = null;

        public ActionResult Index()
        {
            if (customerList == null)
            {
                customerList = new List<Customer>()
                    {
                        new Customer { Id=1010, FullName="Berry Jonson", Email="b.jonson@gmail.com",Phone="(235) 223 1122", DateOfBirth="12-07-1996",Gender= Gender.Female},
                        new Customer { Id=1214, FullName="Stephan Desoza", Email="s.desoza@yahoo.com",Phone="(335) 823 3411", DateOfBirth="18-07-1993",Gender= Gender.Male},
                        new Customer { Id=1337, FullName="Mark Robert", Email="robert.m@live.com",Phone="(235) 324 5532", DateOfBirth="24-06-1998",Gender= Gender.Male},
                        new Customer { Id=1367, FullName="Susan Jermy", Email="susan.jermy@gmail.com",Phone="(761) 822 9201", DateOfBirth="12-07-1999",Gender= Gender.Female},
                        new Customer { Id=2945, FullName="Mary Michael ", Email="mary.michael@gmail.com",Phone="(533) 112 5576", DateOfBirth="12-07-2000",Gender= Gender.Female}
                    };
            }
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Customers");
                TempData["userRights"] = userRights;
                return View(customerList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Delete(int Id)
        {
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Customers");
                if (userRights.DeleteAuthorized)
                {
                    var customerRecord = customerList.Where(x => x.Id == Id).FirstOrDefault();
                    if (customerRecord != null)
                    {
                        customerList.Remove(customerRecord);
                    }
                    return RedirectToAction("Index", customerList);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create(Customer editModel)
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
        public ActionResult AddUpdateCustomer(Customer model)
        {
            if (ModelState.IsValid)
            {
                var userRights = Authorization.GetAuthorizedRights("Customers");

                if (model.Id == 0)
                {
                    if (userRights.AddAuthorized)
                    {
                        customerList.Add(model);
                    }
                }
                else
                {
                    if (userRights.EditAuthorized)
                    {
                        Customer customerRecord = customerList.Where(x => x.Id == model.Id).FirstOrDefault();

                        customerRecord.Id = model.Id;
                        customerRecord.FullName = model.FullName;
                        customerRecord.Email = model.Email;
                        customerRecord.Phone = model.Phone;
                        customerRecord.Gender = model.Gender;
                        customerRecord.DateOfBirth = model.DateOfBirth;
                    }
                }
            }
            ModelState.Clear();

            return RedirectToAction("Index", customerList);
        }
    }
}