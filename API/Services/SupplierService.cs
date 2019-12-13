using API.Services.Interface;
using Data.Model;
using Data.Repository;
using Data.Repository.Interface;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace API.Services
{
    public class SupplierService : ISupplierService
    {

        SupplierRepository _supplierRepository = new SupplierRepository();

        public int Create(SupplierVM supplierVM)
        {
            if (string.IsNullOrWhiteSpace(supplierVM.Name) || (string.IsNullOrWhiteSpace(supplierVM.Email)))
            {
                return 0;
            }
            else
            {
                return _supplierRepository.Create(supplierVM);
            }
        }

        public int Delete(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return 0;
            }
            else
            {
                return _supplierRepository.Delete(id);
            }
        }

        public IEnumerable<Supplier> Get()
        {
            return _supplierRepository.Get();
        }

        public Supplier Get(int id)
        {
            return _supplierRepository.Get(id);
        }

        public int Update(int id, SupplierVM supplierVM)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return 0;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(supplierVM.Name) || (string.IsNullOrWhiteSpace(supplierVM.Email)))
                {
                    return 0;
                }
                else
                {
                    return _supplierRepository.Update(id, supplierVM);
                }
            }
        }
    }
}