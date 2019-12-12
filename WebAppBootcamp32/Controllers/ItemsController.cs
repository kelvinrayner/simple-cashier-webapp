using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBootcamp32.Models;
using WebAppBootcamp32.ViewModel;

namespace WebAppBootcamp32.Controllers
{
    public class ItemsController : Controller
    {
        Bootcamp32Entities myEntities = new Bootcamp32Entities();
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
        public ActionResult Create()
        {
            ViewBag.Supplier = myEntities.TB_M_Supplier.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString()});
            return View();
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

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(ItemVM itemVM)
        {
            try
            {
                TB_M_Item items = new TB_M_Item();
                items.Name = itemVM.Name;
                items.Stock = itemVM.Stock;
                items.Price = itemVM.Price;
                var supplierId = myEntities.TB_M_Supplier.Find(itemVM.Supplier_Id);
                items.TB_M_Supplier = supplierId;
                
                myEntities.TB_M_Item.Add(items);
                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

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
                var supplierId = myEntities.TB_M_Supplier.Find(itemVM.Supplier_Id);
                items.TB_M_Supplier = supplierId;

                myEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
