﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using The_New_Paradise.Filters;
using The_New_Paradise.Models;
namespace The_New_Paradise.Controllers
{
    public class AdminTablesController : Controller
    {
        private ProjectHeavenEntities2 db = new ProjectHeavenEntities2();

        // GET: AdminTables
        [AuthFilter]
        public ActionResult Index()
        {
            return View(db.AdminTables.ToList());
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new Models.AdminTable());
        }
        [HttpPost]
        public ActionResult SignIn(Models.AdminTable obj)
        {
            if (!ModelState.IsValid)
                return View(obj);
            else
            {
                using (var context = new ProjectHeavenEntities2())
                {
                    Models.AdminTable admin = context.AdminTables.Where(u => u.Admin_Email == obj.Admin_Email && u.Admin_Password == obj.Admin_Password).FirstOrDefault();
                    if (admin != null)
                    {

                        Session["AdminEmail"] = admin.Admin_Email;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User Email or Password");
                        return View(obj);
                    }
                }
            }
        }

    }
}