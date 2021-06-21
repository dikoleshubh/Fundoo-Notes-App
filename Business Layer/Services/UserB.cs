using System;
using System.Collections.Generic;
using System.Text;
using Business_Layer.Interfaces;
using Common_Layer;
using Repository_Layer.Interfaces;

namespace Business_Layer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public bool SampleUserApi(User user) //Updating POST Request
        {
            try
            {
                user.FirstName = user.FirstName + "Update From BL";
                return this.userRL.SampleUserApi(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public User AddUser(User user)
        {
            this.userRL.AddUser(user);
            return user;
        }
        public string Login(string email, string password)
        {
            try
            {
                string result = this.userRL.Login(email, password);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ForgotPassword(string email)
        {
            try
            {
                bool result = this.userRL.ForgotPassword(email);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reset Password Method 
        /// </summary>
        /// <param name="resetPassword">variable of type ResetPasswordModel</param>
        /// <returns>boolean result</returns>
        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                bool result = this.userRL.ResetPassword(resetPassword);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string GenerateToken(string Email)
        {
            try
            {
                return this.userRL.GenerateToken(Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
    

