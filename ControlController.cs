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
    public class ControlController : ControllerBase
    {
        private readonly Model.ECommerceDB db;
        public ControlController(Model.ECommerceDB _db)
        {
            db = _db;
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult CheckPermission([FromHeader] string ApiKey, [FromHeader] Control.Enumuration.Type Page, [FromHeader] Control.Enumuration.Operation Operation, [FromHeader] string UserName)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                if (string.IsNullOrEmpty(UserName))
                { 
                    return Ok(Control.Constant.AccessDenied);
                }

                Business.User user_Facades = new Business.User(db);
                
                ViewModel.vm_User user = user_Facades.GetByUserName(UserName);
                if (user == null)
                {
                    return Ok(Control.Constant.AccessDenied);
                }
                if (user_Facades.CheckPermission(user.Id,(int) Page,(int) Operation))
                    return Ok(Control.Constant.SuccessResult);
                else
                    return Ok(Control.Constant.AccessDenied);
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }

        }
    }
}
