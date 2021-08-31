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
    public class MaterialController : Controller
    {
        private readonly ECommerceDB _db;
        public MaterialController(ECommerceDB db)
        {
            _db = db;
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] vm_Material vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Material material = new Business.Material(_db);
                if (vm.Id == 0)
                {
                    return Ok(material.Insert(vm));
                }
                else
                {
                    return Ok(material.Update(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] vm_Material vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Material material = new Business.Material(_db);
                return Ok(material.Delete(vm));
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
                Business.Material material = new Business.Material(_db);
                return Ok(material.GetById(Id));
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
                Business.Material material = new Business.Material(_db);
                return Ok(material.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        ///////////////////////////
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] vm_MaterialDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Material detail = new Business.Material(_db);
                if (vm_detail.Id == 0)
                {
                    return Ok(detail.InsertDetail(vm_detail));
                }
                else
                {
                    return Ok(detail.UpdateDetail(vm_detail));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] vm_MaterialDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Material detail = new Business.Material(_db);
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
                Business.Material detail = new Business.Material(_db);
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
                Business.Material detail = new Business.Material(_db);
                return Ok(detail.GetAllDetail());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
