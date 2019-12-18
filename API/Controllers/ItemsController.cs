using API.Services.Interface;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ItemsController : ApiController
    {
        private IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        // GET: api/Items
        public HttpResponseMessage Get()
        {
            var data = _itemService.Get();
            if (data == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        // GET: api/Items/5
        public HttpResponseMessage Get(int id)
        {
            var data = _itemService.Get(id);
            if (data == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        // POST: api/Items
        public HttpResponseMessage Post(ItemVM itemVM)
        {
            var inserted = _itemService.Create(itemVM);
            if (inserted > 0)
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, inserted);
        }

        [HttpPut]
        // PUT: api/Items/5
        public HttpResponseMessage Put(int id, ItemVM itemVM)
        {
            var updated = _itemService.Update(id, itemVM);
            if (updated > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, updated);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        // DELETE: api/Items/5
        public HttpResponseMessage Delete(int id)
        {
            var deleted = _itemService.Delete(id);
            if (deleted > 0)
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, deleted);
        }
    }
}
