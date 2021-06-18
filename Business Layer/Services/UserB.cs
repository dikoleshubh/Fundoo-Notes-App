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
    }
    
}
