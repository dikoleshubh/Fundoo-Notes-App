using Common_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface IUserRL
    {
        bool SampleUserApi(User user);
        User AddUser(User user);
        //public string Login(string email, string password);
        //public string GenerateToken(string UserEmail);
        public string Login(string email, string password);
        
        public string GenerateToken(string Email);
        public bool ForgotPassword(string email);

       
        public bool ResetPassword(ResetPasswordModel resetPassword);
    }
}
