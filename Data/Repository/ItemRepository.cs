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
    public class ItemRepository : IItemRepository
    {
        MyContext myContext = new MyContext();
        public int Create(ItemVM itemVM)
        {
            var item = myContext.Items.Where(i => i.Name.ToLower() == itemVM.Name.ToLower());
            int result = 0;
            if (item != null)
            {
                var push = new Item(itemVM);
                var supplier = myContext.Suppliers.Find(itemVM.Supplier.Id);
                push.Supplier = supplier;
                myContext.Items.Add(push);
                myContext.SaveChanges();
            }
            return result;
        }

        public int Delete(int id)
        {
            var delete = myContext.Items.Find(id);
            delete.Delete();
            return myContext.SaveChanges();
        }

        public IEnumerable<Item> Get()
        {
            var items = myContext.Items.Include("Supplier").Where(i => i.IsDeleted == false).ToList()/*.Select(s => new Supplier { Id = s.Supplier.Id })*/;
            return items;
        }

        public Item Get(int id)
        {
            return myContext.Items.Find(id);
        }

        public int Update(int id, ItemVM itemVM)
        {
            var update = myContext.Items.Find(id);
            update.Update(itemVM);
            return myContext.SaveChanges();
        }
    }
}
