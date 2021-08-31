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
    public class ProductPhotoController : Controller
    {
        private readonly ECommerceDB _db;
        public ProductPhotoController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey,[FromForm] vm_ProductPhoto vm)
        {
            vm.Log.UserName = vm.UserName;
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.ProductPhoto photo = new Business.ProductPhoto(_db);
                if (vm.Id == Guid.Empty)
                {
                    return Ok(photo.Insert(vm));
                }
                else
                {
                    return Ok(photo.Update(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey,[FromBody] vm_ProductPhoto vm)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.ProductPhoto photo = new Business.ProductPhoto(_db);
                return Ok(photo.Delete(vm));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetByProductId([FromHeader] string ApiKey,[FromHeader]Guid productid)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.ProductPhoto photo = new Business.ProductPhoto(_db);
                return Ok(photo.GetByProductId(productid));
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
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.ProductPhoto photo = new Business.ProductPhoto(_db);
                return Ok(photo.GetById(id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }


    }
}
