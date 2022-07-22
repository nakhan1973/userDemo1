﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class OperationsController : Controller
    {
        private static List<Operation> operationList = null;

        public ActionResult Index()
        {
            if (operationList == null)
            {
                operationList = new List<Operation>()
                    {
                        new Operation { Id=1010, Name="Order- T Shirts Mens", PaymentMethod="COD",StartDate="23 Jun 2022", EndDate="27 Jun 2022",Status= OpStatus.Closed},
                        new Operation { Id=1214, Name="Order- Cosmetics", PaymentMethod="COD",StartDate="27 Jun 2022", EndDate="05 Jul 2022",Status= OpStatus.Delivered},
                        new Operation { Id=1337, Name="Order- Sumsung Mobile A75", PaymentMethod="Credit Card",StartDate="29 Jun 2022", EndDate="09 Jul 2022",Status= OpStatus.Delivered},
                        new Operation { Id=1367, Name="Order- Blue Tooth", PaymentMethod="Paid",StartDate="09 Jul 2022", EndDate="25 Jul 2022",Status= OpStatus.Shipping},
                        new Operation { Id=2945, Name="Order- Baby Suit 24 Size", PaymentMethod="COD",StartDate="14 Jul 2022", EndDate="27 Jul 2022",Status= OpStatus.Packing}
                    };
            }
            if (Authentication.IsUserLoggedIn())
            {
                var userRights = Authorization.GetAuthorizedRights("Operations");
                TempData["userRights"] = userRights;
                return View(operationList);
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
                var userRights = Authorization.GetAuthorizedRights("Operations");
                if (userRights.DeleteAuthorized)
                {
                    var operationRecord = operationList.Where(x => x.Id == Id).FirstOrDefault();
                    if (operationRecord != null)
                    {
                        operationList.Remove(operationRecord);
                    }
                    return RedirectToAction("Index", operationList);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create(Operation editModel)
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
        public ActionResult AddUpdateOperation(Operation model)
        {
            if (ModelState.IsValid)
            {
                var userRights = Authorization.GetAuthorizedRights("Operations");

                if (model.Id == 0)
                {
                    if (userRights.AddAuthorized)
                    {
                        operationList.Add(model);
                    }
                }
                else
                {
                    if (userRights.EditAuthorized)
                    {
                        Operation operationRecord = operationList.Where(x => x.Id == model.Id).FirstOrDefault();

                        operationRecord.Id = model.Id;
                        operationRecord.Name = model.Name;
                        operationRecord.PaymentMethod = model.PaymentMethod;
                        operationRecord.StartDate = model.StartDate;
                        operationRecord.EndDate = model.EndDate;
                        operationRecord.Status = model.Status;
                    }
                }
            }
            ModelState.Clear();

            return RedirectToAction("Index", operationList);
        }
    }
}