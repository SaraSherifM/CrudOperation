using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudOperation.Models;

namespace CrudOperation.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        //assigin this controller a the default controller in the RouteConfig File
        public ActionResult Index()
        {
            return View();

        }
        [HttpGet]
        public ActionResult GetData()
        {
            using (DbModel db = new DbModel())
            {
                var employees = db.EmployeeInfoes.OrderBy(a => a.Name).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            using (DbModel db = new DbModel())
            {
                var v = db.EmployeeInfoes.Where(a => a.EmployeeID == id).FirstOrDefault();
                return View(v);
            }

        }

        [HttpPost]
        public JsonResult Save(EmployeeInfo emp)
        {
            bool status = false;
            if (ModelState.IsValid)//we need to implement validation here will do it from server side
            {
                using (DbModel db = new DbModel())
                {
                    if (emp.EmployeeID > 0) //it means it already exists because we set id=0
                    {
                        var v = db.EmployeeInfoes.Where(a => a.EmployeeID == emp.EmployeeID).FirstOrDefault();
                        if (v != null)
                        {
                            //Edit
                            v.Name = emp.Name;
                            v.Position = emp.Position;
                            v.Office = emp.Office;
                            v.Age = emp.Age;
                            v.Salary = emp.Salary;
                        }

                    }
                    else
                    {
                        //Save
                        db.EmployeeInfoes.Add(emp);
                    }

                    db.SaveChanges();
                    status = true;

                }

            }

            return new JsonResult { Data = new { status = status } };
        }

        //confirm window before deleting
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (DbModel db = new DbModel())
            {
                var v = db.EmployeeInfoes.Where(a => a.EmployeeID == id).FirstOrDefault();
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
        public JsonResult DeleteEmp(int id)
        {
            bool status = false;
            using (DbModel db = new DbModel())
            {
                var v = db.EmployeeInfoes.Where(a => a.EmployeeID == id).FirstOrDefault();
                if (v != null)
                {
                    db.EmployeeInfoes.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            } 
            return new JsonResult { Data = new { status = status } };
        }
    }
}