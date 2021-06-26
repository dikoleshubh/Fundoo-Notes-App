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
using Microsoft.AspNetCore.Cors;


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
        /*
        [HttpPost]
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





        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                //_logger.LogInformation("The API for Forgot Password has accessed");
                var result = this.userBl.ForgotPassword(email);
                if (result == true)
                {
                   // _logger.LogInformation("Link has sent to given gmail to reset password");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Link has sent to the given email address to reset the password" });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unable to sent link to given email address. This Email doesn't exist in database." });
            }
            catch (Exception ex)
            {
                //_logger.LogWarning("Exception encountered while sending link to given mail address" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller method for Reset password method invocation
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                //_logger.LogInformation("The API for Reset Password has accessed");
                var result = this.userBl.ResetPassword(resetPassword);
                if (result == true)
                {
                  //  _logger.LogInformation("Password has reset successfully");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Password Reset Successfull ! ", Data = resetPassword.Email });
                }

                return this.BadRequest(new { Status = false, Message = "Failed to Reset Password. Given Email doesn't exist in database." });
            }
            catch (Exception ex)
            {
                //_logger.LogWarning("Exception encountered while resetting the poassword" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}



