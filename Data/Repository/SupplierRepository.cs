using Data.Context;
using Data.Model;
using Data.Repository.Interface;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        MyContext myContext = new MyContext();
        public int Create(SupplierVM supplierVM)
        {
            var supplier = myContext.Suppliers.Where(s => s.Name.ToLower() == supplierVM.Name.ToLower() || s.Email.ToLower() == supplierVM.Email.ToLower());
            int result = 0;
            if (supplier != null)
            {
                var push = new Supplier(supplierVM);
                myContext.Suppliers.Add(push);
                result = myContext.SaveChanges();
            }
            return result;
        }

        public int Delete(int id)
        {
            var delete = myContext.Suppliers.Find(id);
            delete.Delete();
            return myContext.SaveChanges();
        }

        public IEnumerable<Supplier> Get()
        {
            return myContext.Suppliers.ToList();
        }

        public Supplier Get(int id)
        {
            return myContext.Suppliers.Find(id);
        }

        public int Update(int id, SupplierVM supplierVM)
        {
            var update = myContext.Suppliers.Find(id);
            update.Update(supplierVM);
            return myContext.SaveChanges();
        }
    }
}
