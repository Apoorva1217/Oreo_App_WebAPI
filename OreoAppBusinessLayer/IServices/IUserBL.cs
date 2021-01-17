using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBusinessLayer.IServices
{
    public interface IUserBL
    {
        UserRegistration Login(UserLogin login);
        bool RegisterUser(UserRegistration register);
    }
}
