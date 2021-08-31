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
    public class UserController : ControllerBase
    {
        private readonly Model.ECommerceDB _db;
        public UserController(Model.ECommerceDB db)
        {
            _db = db;
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult Login([FromHeader] string ApiKey, [FromHeader] string UserName, [FromHeader] string Password)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.User user = new Business.User(_db);
                return Ok(user.Login(UserName, Password));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult Search([FromHeader] string ApiKey, [FromQuery] string Search)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.User user = new Business.User(_db);
                return Ok(user.Search(Search));
            }
            else
            {
                return Ok( Control.Constant.InvalidApiKey);
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] ViewModel.vm_User user)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.User user_Facade = new Business.User(_db);
                if (user.Id == Guid.Empty)
                {
                    return Ok(user_Facade.Insert(user));
                }
                else
                {
                    return Ok(user_Facade.Update(user));
                }
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
                Business.User user = new Business.User(_db);
                return Ok(user.GetById(Id));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpDelete]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] ViewModel.vm_User user)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.User user_Facades = new Business.User(_db);
                return Ok(user_Facades.Delete(user));
            }
            else
            {
                return Ok( Control.Constant.InvalidApiKey);
            }
        }
    }
}
