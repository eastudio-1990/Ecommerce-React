using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        private readonly Model.ECommerceDB _db;
        public StoreController(Model.ECommerceDB db)
        {
            _db = db;
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult Get([FromHeader] string ApiKey)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Store store = new Business.Store(_db);
                return Ok(store.Get());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
