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
    }
}
