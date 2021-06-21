using Common_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IUserBL
    {
        bool SampleUserApi(User user);
        User AddUser(User user);
        //public string Login(string email, string password);
        public string Login(string email, string password);
        public bool ResetPassword(ResetPasswordModel resetModel);
        public bool ForgotPassword(string email);
        public string GenerateToken(string email);
    }

}
