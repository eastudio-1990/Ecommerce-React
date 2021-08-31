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
    public class LanguageController : ControllerBase
    {
        private readonly Model.ECommerceDB _db;
        public LanguageController(Model.ECommerceDB db)
        {
            _db = db;
        }


        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] ViewModel.vm_Language language)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Language lang = new Business.Language(_db);
                if (language.Id == 0)
                {
                    return Ok(lang.Insert(language));
                }
                else
                    return Ok(lang.Update(language));
            }
            else
                return Ok(Control.Constant.InvalidApiKey);
        }

        [HttpPost]
        [EnableCors("MyPolicy")]

        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] ViewModel.vm_Language language)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Language lang = new Business.Language(_db);
                return Ok(lang.Delete(language));
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
                Business.Language lang = new Business.Language(_db);
                return Ok(lang.GetById(Id));
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
                Business.Language lang = new Business.Language(_db);
                return Ok(lang.GetAll());
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

    }
}
