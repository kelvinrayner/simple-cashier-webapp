using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.Model;
using Data.Repository;
using Data.Repository.Interface;
using Data.ViewModel;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        ISupplierRepository supplierRepository = new SupplierRepository();
        
        // GET: api/Suppliers
        public IEnumerable<Supplier> Get()
        {
            var data = supplierRepository.Get();
            return data;
        }

        // GET: api/Suppliers/5
        public Supplier Get(int id)
        {
            var data = supplierRepository.Get(id);
            return data;
        }

        // POST: api/Suppliers
        public void Post(SupplierVM supplierVM)
        {
            supplierRepository.Create(supplierVM);
        }

        // PUT: api/Suppliers/5
        public void Put(int id, SupplierVM supplierVM)
        {
            supplierRepository.Update(id, supplierVM);
        }

        // DELETE: api/Suppliers/5
        public void Delete(int id)
        {
            supplierRepository.Delete(id);
        }
    }
}
