using datadatableCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace datadatableCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public string browse(string category)
        {
            string message = HttpUtility.HtmlDecode("Showing Category" + category );
            return message;
        }
        public ActionResult GetEmployees()
        {
            using (myDatabaseEntities dc = new myDatabaseEntities())
            {
                var employees = dc.employees.OrderBy(a => a.FirstName).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (myDatabaseEntities dc = new myDatabaseEntities())
            {
                var v = dc.employees.Where(a => a.EmployeeId == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        public ActionResult Save(employee emp)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (myDatabaseEntities dc = new myDatabaseEntities())
                {
                    if (emp.EmployeeId > 0)
                    {
                        //Edit 
                        var v = dc.employees.Where(a => a.EmployeeId == emp.EmployeeId).FirstOrDefault();
                        if (v != null)
                        {
                            v.FirstName = emp.FirstName;
                            v.LastName = emp.LastName;
                            v.EmailID = emp.EmailID;
                            v.City = emp.City;
                            v.Country = emp.Country;
                            v.Age = emp.Age;
                            v.Income = emp.Income;
                            v.Annual_income = emp.Annual_income;
                        }
                    }
                    else
                    {
                        //Save
                        dc.employees.Add(emp);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (myDatabaseEntities dc = new myDatabaseEntities())
            {
                var v = dc.employees.Where(a => a.EmployeeId == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (myDatabaseEntities dc = new myDatabaseEntities())
            {
                var v = dc.employees.Where(a => a.EmployeeId == id).FirstOrDefault();
                if (v != null)
                {
                    dc.employees.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }






    }
}