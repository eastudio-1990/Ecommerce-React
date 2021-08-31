using E_Commerce_API.Model;
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
    public class ZoneController : ControllerBase
    {
        private readonly ECommerceDB _db;
        public ZoneController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] ViewModel.vm_Zone vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                if (vm.Id == 0)
                {
                    return Ok(zone.Insert(vm));
                }
                else
                {
                    return Ok(zone.Update(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] ViewModel.vm_Zone vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                return Ok(zone.Delete(vm));
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
                Business.Zone zone = new Business.Zone(_db);
                return Ok(zone.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader] string ApiKey, [FromHeader] int id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                return Ok(zone.GetById(id));
            }
            return Ok(Control.Constant.InvalidApiKey);
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveZoneProvince([FromHeader] string ApiKey, [FromBody] ViewModel.vm_ZoneProvince vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                if (vm.Id == 0)
                {
                    return Ok(zone.InsertZoneProvince(vm));
                }
                else
                {
                    return Ok(zone.UpdateZoneProvince(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteZoneProvince([FromHeader] string ApiKey, [FromBody] ViewModel.vm_ZoneProvince vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                return Ok(zone.DeleteZoneProvince(vm));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetZoneProvinceById([FromHeader] string ApiKey, [FromHeader] int id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                return Ok(zone.GetZoneProvinceById(id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetProvinceByZoneId([FromHeader] string ApiKey, [FromHeader] int zoneid)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                return Ok(zone.GetAllProvinceByZoneId(zoneid));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult CheckDuplicate([FromHeader] string ApiKey, [FromHeader] int ZoneId, [FromHeader] int ProvinceId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Zone zone = new Business.Zone(_db);
                return Ok(zone.CheckDuplicate(ProvinceId,ZoneId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
