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
    }

}
