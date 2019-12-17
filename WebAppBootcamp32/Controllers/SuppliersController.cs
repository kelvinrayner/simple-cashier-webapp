using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public JsonResult Create(AddSupplierVM addSupplierVM)
        {
            var client = new HttpClient();
            var myContent = JsonConvert.SerializeObject(addSupplierVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("http://localhost:2414/API/Suppliers", byteContent).Result;
            return Json(result);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int id, AddSupplierVM addSupplierVM)
        {
            var model = new AddSupplierVM();
            model.Suppliers = myEntities.TB_M_Supplier.Find(id);
            return View(model.Suppliers);
        }

        // POST: Suppliers/Edit/5
        public JsonResult UpdateSupplier(AddSupplierVM addSupplierVM)
        {
            var client = new HttpClient();
            var myContent = JsonConvert.SerializeObject(addSupplierVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("http://localhost:2414/API/Suppliers/" + addSupplierVM.Id, byteContent).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        // POST: Suppliers/Delete/5
        public JsonResult Delete(int id)
        {
            var client = new HttpClient();
            var result = client.DeleteAsync("http://localhost:2414/API/Suppliers/" + id).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
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
        
        public JsonResult GetById(int id)
        {
            TB_M_Supplier supplier = null;
            var client = new HttpClient();
            var responseTask = client.GetAsync("http://localhost:2414/API/Suppliers/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TB_M_Supplier>();
                readTask.Wait();
                supplier = readTask.Result;
            }
            else
            {
                // try to find something
            }

            return Json(supplier, JsonRequestBehavior.AllowGet);
        }

    }
}
