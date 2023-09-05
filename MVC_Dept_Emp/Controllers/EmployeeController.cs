using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Dept_Emp.Models;

namespace MVC_Dept_Emp.Controllers
{
    public class EmployeeController : Controller
    {
        IConfiguration configuration;
        EmployeeCrud empCrud;
        DepartmentCrud deptCrud;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public EmployeeController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.configuration = configuration;
            empCrud = new EmployeeCrud(this.configuration);
            deptCrud = new DepartmentCrud(this.configuration);
            this.env = env;
        }


        // GET: EmployeeController
        public ActionResult Index()
        {
            return View(empCrud.GetEmployees());
        }


        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View(empCrud.GetEmployeeById(id));
        }


        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            ViewBag.Departments = deptCrud.GetDepartments();
            return View();
        }


        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp, IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                emp.Imageurl = "~/images/" + file.FileName;
                var result=empCrud.AddEmployee(emp);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var emp = empCrud.GetEmployeeById(id);
            ViewBag.Departments = deptCrud.GetDepartments();
            HttpContext.Session.SetString("oldImageUrl", emp.Imageurl);
            return View(emp);
        }


        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp, IFormFile file)
        {
            try
            {
                string oldimageurl = HttpContext.Session.GetString("oldImageUrl");
                if (file != null)
                {
                    using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                    }
                    emp.Imageurl = "~/images/" + file.FileName;


                    string[] str = oldimageurl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    emp.Imageurl = oldimageurl;
                }
                var result = empCrud.UpdateEmployee(emp);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(empCrud.GetEmployeeById(id));
        }


        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var emp = empCrud.GetEmployeeById(id);
                string[] str = emp.Imageurl.Split("/");
                string str1 = (str[str.Length - 1]);
                string path = env.WebRootPath + "\\images\\" + str1;
                System.IO.File.Delete(path);
                var result = empCrud.DeleteEmployee(id);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
