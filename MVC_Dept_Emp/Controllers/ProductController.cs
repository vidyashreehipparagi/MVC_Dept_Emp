using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Dept_Emp.Models;

namespace MVC_Dept_Emp.Controllers
{
    public class ProductController : Controller
    {
        IConfiguration configuration;
        ProductCrud productCrud;
        CategoryCrud categoryCrud;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public ProductController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.configuration = configuration;
            productCrud = new ProductCrud(this.configuration);
            categoryCrud = new CategoryCrud(this.configuration);
            this.env = env;
        }


        // GET: EmployeeController
        public ActionResult Index()
        {
            return View(productCrud.GetProducts());
        }


        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View(productCrud.GetProductById(id));
        }


        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            ViewBag.Category= categoryCrud.GetAllCategory();
            return View();
        }


        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                product.Imageurl = "~/images/" + file.FileName;
                var result = productCrud.AddProduct(product);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }


        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = productCrud.GetProductById(id);
            ViewBag.Category = categoryCrud.GetAllCategory();
            HttpContext.Session.SetString("oldImageUrl", product.Imageurl);
            return View(product);
        }


        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, IFormFile file)
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
                    product.Imageurl = "~/images/" + file.FileName;


                    string[] str = oldimageurl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    product.Imageurl = oldimageurl;
                }
                var result = productCrud.UpdateProduct(product);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }


        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(productCrud.GetProductById(id));
        }


        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var product = productCrud.GetProductById(id);
                string[] str = product.Imageurl.Split("/");
                string str1 = (str[str.Length - 1]);
                string path = env.WebRootPath + "\\images\\" + str1;
                System.IO.File.Delete(path);
                var result = productCrud.DeleteProduct(id);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }

    }
}
