﻿using Data.Models;
using Data.ViewModel;
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

namespace WebAppBootcamp32.Controllers
{
    public class ItemsController : Controller
    {
        Bootcamp32Entities myEntities = new Bootcamp32Entities();
        int counter = 0;
        // GET: Items
        public ActionResult Index()
        {
            var itemsList = myEntities.TB_M_Item.ToList();
            return View(itemsList);
        }

        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            var items = myEntities.TB_M_Item.Find(id);
            return View(items);
        }

        // GET: Items/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Supplier = myEntities.TB_M_Supplier.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString()});
        //    return View();
        //}
        public JsonResult Create(ItemVM itemVM)
        {
            var client = new HttpClient();
            var myContent = JsonConvert.SerializeObject(itemVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("http://localhost:2414/API/Items", byteContent).Result;
            if (result.IsSuccessStatusCode)
            {
                counter++;
            }
            return Json(counter);
        }


        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            var items = myEntities.TB_M_Item.Find(id);
            ViewBag.Supplier = myEntities.TB_M_Supplier.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString(), Selected = s.Id == id });
            return View(items);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var items = myEntities.TB_M_Item.Find(id);
                myEntities.TB_M_Item.Remove(items);
                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// POST: Items/Create
        //[HttpPost]
        //public ActionResult Create(ItemVM itemVM)
        //{
        //    try
        //    {
        //        TB_M_Item items = new TB_M_Item();
        //        items.Name = itemVM.Name;
        //        items.Stock = itemVM.Stock;
        //        items.Price = itemVM.Price;
        //        var supplierId = myEntities.TB_M_Supplier.Find(itemVM.Supplier);
        //        items.TB_M_Supplier = supplierId;
                
        //        myEntities.TB_M_Item.Add(items);
        //        myEntities.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ItemVM itemVM)
        {
            try
            {
                var items = myEntities.TB_M_Item.Find(id);
                items.Name = itemVM.Name;
                items.Stock = itemVM.Stock;
                items.Price = itemVM.Price;
                var supplierId = myEntities.TB_M_Supplier.Find(itemVM.Supplier);
                items.TB_M_Supplier = supplierId;

                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<JsonResult> itemJsonList()
        {
            string url = "http://localhost:2414/API/Items";
            var client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsAsync<IEnumerable<ItemVM>>();
                var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        //POST: Suppliers/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        var items = myEntities.TB_M_Item.Find(id);
        //        myEntities.TB_M_Item.Remove(items);
        //        myEntities.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public JsonResult Delete(int? ID)
        //{
        //    var items = myEntities.TB_M_Item.Find(ID);
        //    if (ID == null)
        //    {
        //        return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
        //    }
        //    myEntities.TB_M_Item.Remove(items);
        //    myEntities.SaveChanges();

        //    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);   
        //}

        // GET: Demo
        public ActionResult ShowGrid()
        {
            return View();
        }

        public JsonResult LoadSupplier()
        {
            IEnumerable<TB_M_Supplier> suppliers = null;
            var client = new HttpClient();
            var responseTask = client.GetAsync("http://localhost:2414/API/Suppliers/");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<TB_M_Supplier>>();
                readTask.Wait();
                suppliers = readTask.Result;
            }
            else
            {
                suppliers = Enumerable.Empty<TB_M_Supplier>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(suppliers, JsonRequestBehavior.AllowGet);
        }
    }
}
