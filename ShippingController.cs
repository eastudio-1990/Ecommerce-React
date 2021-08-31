using E_Commerce_API.Model;
using E_Commerce_API.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShippingController : Controller
    {
        private readonly ECommerceDB _db;
        public ShippingController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey,[FromBody] vm_Shipping vm_Shipping)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Shipping shipping = new Business.Shipping(_db);
                if (vm_Shipping.Id==0)
                {
                return Ok(shipping.Insert(vm_Shipping));
                }
                else
                {
                    return Ok(shipping.Update(vm_Shipping));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
            

        }
        [HttpDelete]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] vm_Shipping vm_Shipping)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Shipping shipping = new Business.Shipping(_db);
                return Ok(shipping.Delete(vm_Shipping));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader] string ApiKey,[FromHeader] int Id)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Shipping shipping = new Business.Shipping(_db);
                return Ok(shipping.GetById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAll([FromHeader] string ApiKey)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Shipping shipping = new Business.Shipping(_db);
                return Ok(shipping.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
