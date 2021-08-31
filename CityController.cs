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
    public class CityController : Controller
    {
        private readonly ECommerceDB _db;
        public CityController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] vm_City vm_city)
         {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.City city = new Business.City(_db);
                if (vm_city.Id == 0)
                {
                    return Ok(city.Insert(vm_city));
                }
                else
                {
                    return Ok(city.Update(vm_city));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] vm_City vm_city)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.City city = new Business.City(_db);
                return Ok(city.Delete(vm_city));
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
                Business.City city = new Business.City(_db);
                return Ok(city.GetById(Id));
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
            if (ApiKey==Control.Constant.ApiKey)
            {
                Business.City city = new Business.City(_db);
                return Ok(city.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        //////////////////

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] vm_CityDetail cityDetail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.City city = new Business.City(_db);
                if (cityDetail.id == 0)
                {
                    return Ok(city.InsertDetail(cityDetail));
                }
                else
                {
                    return Ok(city.UpdateDetail(cityDetail));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpDelete]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] vm_CityDetail cityDetail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.City city = new Business.City(_db);
                return Ok(city.DeleteDetail(cityDetail));
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
                Business.City city = new Business.City(_db);
                return Ok(city.GetByIdDetail(Id));
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
                Business.City city = new Business.City(_db);
                return Ok(city.GetAllDetail());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }


    }
}
