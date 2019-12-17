using API.Services.Interface;
using Data.Model;
using Data.Repository;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Services
{
    public class ItemService : IItemService
    {
        ItemRepository _itemRepository = new ItemRepository();

        public int Create(ItemVM itemVM)
        {
            
            if (string.IsNullOrWhiteSpace(itemVM.Name) || string.IsNullOrWhiteSpace(itemVM.Price.ToString()) || string.IsNullOrWhiteSpace(itemVM.Stock.ToString()))
            {
                return 0;
            }
            else
            {
                return _itemRepository.Create(itemVM);
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
                return _itemRepository.Delete(id);
            }
        }

        public IEnumerable<Item> Get()
        {
            return _itemRepository.Get();
        }

        public Item Get(int id)
        {
            return _itemRepository.Get(id);
        }

        public int Update(int id, ItemVM itemVM)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return 0;
            }
            else
            {
                if(string.IsNullOrWhiteSpace(itemVM.Name) || string.IsNullOrWhiteSpace(itemVM.Price.ToString()) || string.IsNullOrWhiteSpace(itemVM.Stock.ToString()))
                {
                    return 0;
                }
                else
                {
                    return _itemRepository.Update(id, itemVM);
                }
            }
        }
    }
}