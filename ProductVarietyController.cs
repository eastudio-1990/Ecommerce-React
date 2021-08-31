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
    public class ProductVarietyController : ControllerBase
    {
        private readonly Model.ECommerceDB _db;
        public ProductVarietyController(Model.ECommerceDB db)
        {
            _db = db;
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] ViewModel.vm_ProductVariety variety)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductVariety pVariety = new Business.ProductVariety(_db);
                if (variety.Id == Guid.Empty)
                {
                    return Ok(pVariety.Insert(variety));
                }
                else
                {
                    return Ok(pVariety.Update(variety));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] ViewModel.vm_ProductVariety variety)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductVariety pVariety = new Business.ProductVariety(_db);
                return Ok(pVariety.Delete(variety));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader] string ApiKey, [FromHeader] Guid id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductVariety pVariety = new Business.ProductVariety(_db);
                return Ok(pVariety.GetById(id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetByProductId([FromHeader] string ApiKey,[FromHeader]Guid ProductId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.ProductVariety pVariety = new Business.ProductVariety(_db);
                return Ok(pVariety.GetByProductId(ProductId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
