using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Business_Layer.Interfaces;
using Common_Layer;

namespace Fundo_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBl;
        public UserController(IUserBL userBl)
        {
            this.userBl = userBl;
        }
        //[HttpPost]
        //public ActionResult SampleUserApi(User user)
        //{
        //    try
        //    {

        //        bool res = this.userBl.SampleUserApi(user);
        //        if (res == true)
        //        {
        //            return this.Ok(new { success = true, message = "Registration Successful " });
        //        }
        //        else
        //        {
        //            return this.Ok(new { success = false, message = "Registration UnSuccessful " });
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        return this.BadRequest(new { success = false, message = e.Message });
        //    }
        //}
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            try
            {
                this.userBl.AddUser(user);
                return this.Ok(new { success = true, message = "User Registration Successful " });
            }

            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message, innerExeption = e.InnerException });
            }
        }

    }
}
