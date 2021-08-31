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
    public class CountryController : Controller
    {
        private readonly Model.ECommerceDB _db;
        public CountryController(Model.ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey,[FromBody] vm_Country vm)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Country country = new Business.Country(_db);
                if (vm.Id==0)
                {
                    return Ok(country.Insert(vm));
                }
                else
                {
                    return Ok(country.Update(vm));
                }
            }
            else
              {
                return Ok(Control.Constant.InvalidApiKey);
              }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey,[FromBody] vm_Country vm_country)
        {
            if (ApiKey == Control.Constant.ApiKey)
             {
                Business.Country country = new Business.Country(_db);
                return Ok(country.Delete(vm_country));
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
                Business.Country country = new Business.Country(_db);
                return Ok(country.GetById(Id));
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
                Business.Country country = new Business.Country(_db);
                return Ok(country.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        ///////////////////////////
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] vm_CountryDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Country detail = new Business.Country(_db);
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
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] vm_CountryDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Country detail = new Business.Country(_db);
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
                Business.Country detail = new Business.Country(_db);
                return Ok(detail.GetDetailById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAllDetail([FromHeader] string ApiKey,[FromHeader] int CountryId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Country detail = new Business.Country(_db);
                return Ok(detail.GetDetailAll(CountryId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult CheckDuplicate([FromHeader] string ApiKey,[FromHeader]int id)
        {
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.Country country = new Business.Country(_db);
                return Ok(country.CheckDuplicate(id));
            }
            return Ok(Control.Constant.InvalidApiKey);
        }

    }
}
