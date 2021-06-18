using Common_Layer;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Services
{
    public class UserR : IUserRL
    {
        IList<User> Users = new List<User>();
        public bool SampleUserApi(User user)
        {
            try
            {
                Users.Add(user);
                if (Users.Contains(user) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private UserContext _userDbContext;
        public UserR(UserContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public User AddUser(User user)
        {
            _userDbContext.Users.Add(user);
            _userDbContext.SaveChanges();
            return user;
        }

    }
}
