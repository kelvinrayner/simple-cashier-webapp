﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using API.Services;
using API.Services.Interface;
using Data.Model;
using Data.Repository;
using Data.Repository.Interface;
using Data.ViewModel;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        //ISupplierRepository supplierRepository = new SupplierRepository();
        //ISupplierService supplierService = new SupplierService();

        private ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [System.Web.Mvc.HttpGet]
        // GET: api/Suppliers
        public HttpResponseMessage Get()
        {
            var data = _supplierService.Get();
            if (data == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [System.Web.Mvc.HttpGet]
        // GET: api/Suppliers/5
        public HttpResponseMessage Get(int id)
        {
            var data = _supplierService.Get(id);
            if (data == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [System.Web.Mvc.HttpPost]
        // POST: api/Suppliers
        public HttpResponseMessage Post(SupplierVM supplierVM)
        {
            var inserted = _supplierService.Create(supplierVM);
            if (inserted > 0)
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, inserted);
        }

        [System.Web.Mvc.HttpPut]
        // PUT: api/Suppliers/5
        public HttpResponseMessage Put(int id, SupplierVM supplierVM)
        {
            var updated = _supplierService.Update(id, supplierVM);
            if (updated > 0)
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, updated);
        }

        [System.Web.Mvc.HttpDelete]
        // DELETE: api/Suppliers/5
        public HttpResponseMessage Delete(int id)
        {
            var deleted = _supplierService.Delete(id);
            if (deleted > 0)
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, deleted);
        }
    }
}
