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
    public class CategoryController : Controller
    {
        private readonly ECommerceDB _db;
        public CategoryController(ECommerceDB db)
        {
            _db = db;
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromForm] vm_Category vm_Category)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Category category = new Business.Category(_db);
                vm_Category.Log.UserName = vm_Category.UserName;
                if (vm_Category.Id == 0)
                {
                    return Ok(category.Insert(vm_Category));
                }
                else
                {
                    return Ok(category.Update(vm_Category));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] vm_Category vm_Category)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Category category = new Business.Category(_db);
                return Ok(category.Delete(vm_Category));
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
                Business.Category category = new Business.Category(_db);
                return Ok(category.GetById(id));
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
                Business.Category category = new Business.Category(_db);
                return Ok(category.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        ///////////////////////////////
        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult SaveDetail([FromHeader] string ApiKey, [FromBody] vm_CategoryDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Category detail = new Business.Category(_db);
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
        public IActionResult DeleteDetail([FromHeader] string ApiKey, [FromBody] vm_CategoryDetail vm_detail)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Category category = new Business.Category(_db);
                return Ok(category.DeleteDetail(vm_detail));
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
                Business.Category detail = new Business.Category(_db);
                return Ok(detail.GetDetailById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAllDetail([FromHeader] string ApiKey, [FromHeader] int CategoryId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Category detail = new Business.Category(_db);
                return Ok(detail.GetDetailAll(CategoryId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetByParentId([FromHeader] string ApiKey, [FromHeader] int ParentId)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Category cat = new Business.Category(_db);
                return Ok(cat.GetByParentId(ParentId));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

    }
}
