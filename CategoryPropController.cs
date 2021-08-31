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
    public class CategoryPropController : Controller
    {
        private readonly ECommerceDB _db;
        public CategoryPropController(ECommerceDB db)
        {
            _db = db;
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] vm_CategoryProp vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                if (vm.Id == Guid.Empty)
                {
                    return Ok(prop.Insert(vm));
                }
                else
                {
                    return Ok(prop.Update(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey,[FromBody] vm_CategoryProp vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);

                return Ok(prop.Delete(vm));

            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }

        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader] string ApiKey, [FromHeader] Guid Id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                return Ok(prop.GetById(Id));
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
                Business.CategoryProp Prop = new Business.CategoryProp(_db);
                return Ok(Prop.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetByCategoryId([FromHeader] string ApiKey, [FromHeader] int categoryid)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                return Ok(prop.GetByCategoryId(categoryid));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        ///////////
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] vm_CategoryPropDetail vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                if (vm.Id == Guid.Empty)
                {
                    return Ok(prop.InsertDetail(vm));
                }
                else
                {
                    return Ok(prop.UpdateDetail(vm));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] vm_CategoryPropDetail vm)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                return Ok(prop.DeleteDetail(vm));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetByIdDetail([FromHeader] string ApiKey, [FromHeader] Guid Id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                return Ok(prop.GetByIdDetail(Id));
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
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                return Ok(prop.GetAllDetail());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetDetailByCategoryPropId([FromHeader] string ApiKey, [FromHeader] Guid CategoryPropId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.CategoryProp prop = new Business.CategoryProp(_db);
                return Ok(prop.GetDetailByCategoryProp(CategoryPropId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
    }
}