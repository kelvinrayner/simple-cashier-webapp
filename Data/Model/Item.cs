using Data.Base;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Item : BaseModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public Supplier Supplier { get; set; }

        public Item()
        {

        }

        public Item(ItemVM itemVM)
        {
            this.Name = itemVM.Name;
            this.Stock = itemVM.Stock;
            this.Price = itemVM.Price;
            this.Supplier = itemVM.Supplier;
            this.CreateDate = DateTimeOffset.Now;
            this.IsDeleted = false;
        }

        public void Update(ItemVM itemVM)
        {
            this.Name = itemVM.Name;
            this.Stock = itemVM.Stock;
            this.Price = itemVM.Price;
            this.Supplier = itemVM.Supplier;
            this.UpdateDate = DateTimeOffset.Now;
        }

        public void Delete()
        {
            this.IsDeleted = true;
            this.DeleteDate = DateTimeOffset.Now;

        }
    }
}
