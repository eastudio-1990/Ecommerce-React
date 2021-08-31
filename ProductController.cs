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
    public class ProductController : Controller
    {
        private readonly ECommerceDB _db;
        public ProductController(ECommerceDB db)
        {
            _db = db;
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string Apikey,[FromBody] vm_Product vm)
        {
            if (Apikey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                if (vm.Id == Guid.Empty)
                {
                    return Ok(product.Insert(vm));
                }
                else { 
                    return Ok(product.Update(vm));
                  }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string Apikey, [FromBody] vm_Product vm)
        {
            if (Apikey == Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
              
                    return Ok(product.Delete(vm));               
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader] string Apikey,[FromHeader] Guid Id)
        {
            if (Apikey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                return Ok(product.GetById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAll([FromHeader]string APiKey)
        {
            if (APiKey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                return Ok(product.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult Search([FromHeader] string ApiKey,[FromQuery] string search)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                return Ok(product.Search(search));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SavePublishState([FromHeader] string ApiKey,[FromBody] vm_Product vm)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                return Ok(product.SavePublishState(vm));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        /////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey,[FromBody] vm_ProductDetail vm)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                if (vm.Id==Guid.Empty)
                {
                    return Ok(product.InsertDetail(vm));
                }
                else
                {
                    return Ok(product.UpdateDetail(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpDelete]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteDetail([FromHeader] string ApiKey,[FromBody]vm_ProductDetail vm)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                return Ok(product.DeleteDetail(vm));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }    
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetDetailById([FromHeader] string ApiKey,[FromHeader]Guid Id)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                return Ok(product.GetDetailById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetDetailByProductId([FromHeader] string ApiKey,[FromHeader]Guid ProductId)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Product product = new Business.Product(_db);
                return Ok(product.GetDetailByProductId(ProductId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
