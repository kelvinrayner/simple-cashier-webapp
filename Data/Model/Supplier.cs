using Data.Base;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Supplier : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Supplier()
        {
        }

        public Supplier(SupplierVM supplierVM)
        {
            this.Name = supplierVM.Name;
            this.Email = supplierVM.Email;
            this.CreateDate = DateTimeOffset.Now;
            this.IsDeleted = false;
        }

        public void Update(SupplierVM supplierVM)
        {
            this.Name = supplierVM.Name;
            this.Email = supplierVM.Email;
            this.UpdateDate = DateTimeOffset.Now;
        }

        public void Delete()
        {
            this.IsDeleted = true;
            this.DeleteDate = DateTimeOffset.Now;
        }
    }
}
