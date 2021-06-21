using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Business_Layer.Interfaces;
using Common_Layer;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authorization;

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
        [HttpPost("Regestration")]
        public ActionResult UserRegistration([FromBody] User user)
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

        /*[HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            //_logger.LogInformation("The API for User Login has accessed");
            try
            {
                var message = this.userBl.Login(login.Email, login.Password);
                if (message.Equals("LOGIN SUCCESS"))
                {
                   // _logger.LogInformation("User Logged Login Successfull!");
                    string tokenString = this.userBl.GenerateToken(login.Email);
                    return this.Ok(new { Status = true, Message = message, Data = login.Email, tokenString });
                }

                return this.BadRequest(new ResponseModel<LoginModel>() { Status = false, Message = message });
            }
            catch (Exception ex)
            {
                //_logger.LogWarning("Exception encountered while log in " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }*/



        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser(LoginModel emailModel)
        {
            var token = this.userBl.Login(emailModel.Email, emailModel.Password);
            if (token == null)
                return Unauthorized();
            return this.Ok(new { token = token, success = true, message = "Token Generated Successfull" });
        }









        /// <summary>
        /// request for forgot password
        /// </summary>
        /// <param name="emailAddress"></emailAddress>
        /// <returns></returns>
        [HttpPost]
        [Route("api/forgetPassword")]
        public IActionResult ForgotPassword(string emailAddress)
        {
            try
            {
                var result = this.userBl.SendEmail(emailAddress);
                if (result.Equals(true))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Mail Sent Sucessfully", Data = emailAddress });
                }

                return this.BadRequest(new { Status = false, Message = "Email is not correct:Please enter valid email" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// request for reset password
        /// </summary>
        /// <param name="resetModel"></resetModel>
        /// <returns></returns>
        [HttpPost]
        [Route("api/resetpassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel resetModel)
        {
            try
            {
                bool result = this.userBl.ResetPassword(resetModel);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<ResetPasswordModel>() { Status = true, Message = "Password Changed" });
                }

                return this.BadRequest(new ResponseModel<ResetPasswordModel>() { Status = false, Message = "cannot change password" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<ResetPasswordModel>() { Status = false, Message = ex.Message });
            }
        }
    }

}

