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
    public class RoleController : ControllerBase
    {

        private readonly Model.ECommerceDB _db;
        public RoleController(Model.ECommerceDB db)
        {
            _db = db;

        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Save([FromHeader] string ApiKey, [FromBody] ViewModel.vm_Role role)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Role role_Facades = new Business.Role(_db);
                if (role.Id == 0)
                {
                    return Ok(role_Facades.Insert(role));
                }
                else
                {
                    return Ok(role_Facades.Update(role));
                }
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);

            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Delete([FromHeader] string ApiKey, [FromBody] ViewModel.vm_Role role)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Role role_Facades = new Business.Role(_db);
                return Ok(role_Facades.Delete(role));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);

            }
        }


        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetById([FromHeader]string ApiKey,[FromHeader] int Id)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                Business.Role role_Facades = new Business.Role(_db);
                return Ok(role_Facades.GetById(Id));
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
                Business.Role role_Facades = new Business.Role(_db);
                return Ok(role_Facades.GetAll(0));
            }
            else
            {
                return Ok(Control.Constant.InvalidApiKey);
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetPermissionByRoleId([FromHeader] string ApiKey, [FromHeader] int RoleId, [FromHeader] string UserName)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                // ViewModel.vm_User user = Business.User.GetByUserName(UserName);
                //if (Business.User.CheckPermission(user.Id, Control.Enumuration.Type.Role, Control.Enumuration.Operation.Show))
                // {
                Business.Role role = new Business.Role(_db);

                    return Ok(role.GetPermissionByRoleId(RoleId));
               // }
               // else
               //     return Ok(Control.Const.AccessDenied);
            }
            else
                return Ok(Control.Constant.InvalidApiKey);
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult InsertPermission([FromHeader] string ApiKey, [FromBody] ViewModel.vm_RolePermission permission)
        {
            if (ApiKey == Control.Constant.ApiKey)
            {
                //  ViewModel.vm_User user = Business.User.GetByUserName(permission.log.UserName);
                //  if (Business.User.CheckPermission(user.Id, Control.Enumuration.Type.Role, Control.Enumuration.Operation.Insert))
                //  {
                Business.Role role = new Business.Role(_db);
                    return Ok(role.InsertPermission(permission));
               // }
              //  else
               //     return Ok(Control.Const.AccessDenied);
            }
            else
                return Ok(Control.Constant.InvalidApiKey);
        }

    }
}
