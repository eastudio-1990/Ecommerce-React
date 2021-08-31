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
    public class WeightUnitController : ControllerBase
    {
        private readonly Model.ECommerceDB db;
        public WeightUnitController(Model.ECommerceDB _db)
        {
            db = _db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] ViewModel.vm_WeightUnit unit)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.WeightUnit unit_Facades = new Business.WeightUnit(db);
                if (unit.Id == 0)
                {
                    return Ok(unit_Facades.Insert(unit));
                }
                else
                {
                    return Ok(unit_Facades.Update(unit));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] ViewModel.vm_WeightUnit unit)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.WeightUnit unit_Facades = new Business.WeightUnit(db);
                return Ok( unit_Facades.Delete(unit));
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
                Business.WeightUnit unit = new Business.WeightUnit(db);
                return Ok(unit.GetById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAll([FromHeader]string ApiKey)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.WeightUnit unit = new Business.WeightUnit(db);
                return Ok(unit.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }


        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] ViewModel.vm_WeightUnitDetail unit)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.WeightUnit unit_Facades = new Business.WeightUnit(db);
                if (unit.Id == 0)
                {
                    return Ok(unit_Facades.InsertDetail(unit));
                }
                else
                {
                    return Ok(unit_Facades.UpdateDetail(unit));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] ViewModel.vm_WeightUnitDetail unit)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.WeightUnit unit_Facades = new Business.WeightUnit(db);
                return Ok(unit_Facades.DeleteDetail(unit));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetDetailById([FromHeader] string ApiKey, [FromHeader] int Id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.WeightUnit unit = new Business.WeightUnit(db);
                return Ok(unit.GetDetailById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetDetailAll([FromHeader] string ApiKey,[FromHeader]int WeightUnitId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.WeightUnit unit = new Business.WeightUnit(db);
                return Ok(unit.GetDetailAll(WeightUnitId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}
