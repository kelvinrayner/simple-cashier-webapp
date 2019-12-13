using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppBootcamp32.Models;
using WebAppBootcamp32.ViewModel;

namespace WebAppBootcamp32.Controllers
{
    public class SuppliersController : Controller
    {
        Bootcamp32Entities myEntities = new Bootcamp32Entities();
        // GET: Suppliers
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Supplier = myEntities.TB_M_Supplier.ToList();
            return View(supplierJsonList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int id)
        {
            var supplier = myEntities.TB_M_Supplier.Find(id);
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        public ActionResult Create(AddSupplierVM addSupplierVM)
        {
            try
            {
                TB_M_Supplier supplier = new TB_M_Supplier();
                supplier.Name = addSupplierVM.Name;
                supplier.Email = addSupplierVM.Email;
                supplier.CreateDate = DateTime.Now;

                myEntities.TB_M_Supplier.Add(supplier);
                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int id, AddSupplierVM addSupplierVM)
        {
            var model = new AddSupplierVM();
            model.Suppliers = myEntities.TB_M_Supplier.Find(id);
            return View(model.Suppliers);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        public ActionResult EditSupplier(int id, AddSupplierVM addSupplierVM)
        {
            try
            {
                var supplier = myEntities.TB_M_Supplier.Find(id);
                supplier.Name = addSupplierVM.Name;
                supplier.Email = addSupplierVM.Email;
                supplier.CreateDate = DateTime.Now;
                
                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var supplier = myEntities.TB_M_Supplier.Find(id);
                myEntities.TB_M_Supplier.Remove(supplier);
                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Suppliers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var supplier = myEntities.TB_M_Supplier.Find(id);
                myEntities.TB_M_Supplier.Remove(supplier);
                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<JsonResult> supplierJsonList()
        {
            string url = "http://localhost:2414/API/Suppliers";
            var client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsAsync<List<TB_M_Supplier>>();
                var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            //var get = from s in myEntities.TB_M_Supplier
            //          select new { s.Name, s.Email, s.CreateDate };
            //var json = JsonConvert.SerializeObject(get, new JsonSerializerSettings
            //{
            //    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            //    Formatting = Formatting.Indented
            //});
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
    }
}
