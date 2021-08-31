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
    public class ProvinceController : Controller
    {
        private readonly ECommerceDB _db;
        public ProvinceController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] vm_Province vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Province province = new Business.Province(_db);
                if (vm.Id == 0)
                {
                    return Ok(province.Insert(vm));
                }
                else
                {
                    return Ok(province.Update(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] vm_Province vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Province province = new Business.Province(_db);
                return Ok(province.Delete(vm));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader] string ApiKey, [FromHeader] int Id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Province province = new Business.Province(_db);
                return Ok(province.GetById(Id));
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
                Business.Province province = new Business.Province(_db);
                return Ok(province.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        /// ///////////////////   
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] vm_ProvinceDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Province detail = new Business.Province(_db);
                if (vm_detail.Id == 0)
                {
                    return Ok(detail.InsertDetail(vm_detail));
                }
                else
                {
                    return Ok(detail.UpdateDetail(vm_detail)); ;
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] vm_ProvinceDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Province detail = new Business.Province(_db);
                return Ok(detail.DeleteDetail(vm_detail));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetByIdDetail([FromHeader] string ApiKey, [FromHeader] int Id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Province detail = new Business.Province(_db);
                return Ok(detail.GetByIdDetail(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAllDetail([FromHeader] string ApiKey)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Province detail = new Business.Province(_db);
                return Ok(detail.GetByAllDetail());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
