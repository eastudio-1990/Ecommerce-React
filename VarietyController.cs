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
    public class VarietyController : Controller
    {
        private readonly ECommerceDB _db;
        public VarietyController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public  IActionResult Save([FromHeader] string ApiKey,[FromBody] vm_Variety vm_Variety)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Variety variety = new Business.Variety(_db);
                if (vm_Variety.Id == 0)
                {
                    return Ok(variety.Insert(vm_Variety));
                }
                else
                {
                    return Ok(variety.Update(vm_Variety));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey,[FromBody] vm_Variety vm_Variety)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Variety variety = new Business.Variety(_db);
                return Ok(variety.Delete(vm_Variety));
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
                Business.Variety variety = new Business.Variety(_db);
                return Ok(variety.GetById(Id));
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
                Business.Variety variety = new Business.Variety(_db);
                return Ok(variety.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        ///////       
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] vm_VarietyDetail vm_Variety)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Variety variety = new Business.Variety(_db);
                if (vm_Variety.Id == 0)
                {
                    return Ok(variety.InsertDetail(vm_Variety));
                }
                else
                {
                    return Ok(variety.UpdateDetail(vm_Variety));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpDelete]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] vm_VarietyDetail vm_Variety)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Variety variety = new Business.Variety(_db);
                return Ok(variety.DeleteDetail(vm_Variety));
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
                Business.Variety variety = new Business.Variety(_db);
                return Ok(variety.GetByIdDetail(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAllDetail([FromHeader] string ApiKey, [FromHeader] int VarietyId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Variety variety = new Business.Variety(_db);
                return Ok(variety.GetDetailAll(VarietyId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }


    }
}
