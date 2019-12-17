using Data.Model;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interface
{
    public interface IItemRepository
    {
        IEnumerable<Item> Get();
        Item Get(int id);
        int Create(ItemVM itemVM);
        int Update(int id, ItemVM itemVM);
        int Delete(int id);
    }
}
