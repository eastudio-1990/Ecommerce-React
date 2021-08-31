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
    public class ProductPropertyController : Controller
    {
        private readonly ECommerceDB _db;
        public ProductPropertyController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, vm_ProductProperty vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductProperty productproperty = new Business.ProductProperty(_db);
                if (vm.Id == Guid.Empty)
                {
                    return Ok(productproperty.Insert(vm));
                }
                else
                {
                    return Ok(productproperty.Update(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, vm_ProductProperty vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductProperty productproperty = new Business.ProductProperty(_db);
                return Ok(productproperty.Delete(vm));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader] string ApiKey,[FromHeader] Guid id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductProperty productProperty = new Business.ProductProperty(_db);
                return Ok(productProperty.GetById(id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetByProductId([FromHeader] string ApiKey,[FromHeader] Guid ProductId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductProperty productProperty = new Business.ProductProperty(_db);
                return Ok(productProperty.GetByProductId(ProductId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
       

    }
}
