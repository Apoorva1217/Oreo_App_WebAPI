using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppRepositoryLayer.IServices
{
    public interface IUserRL
    {
        bool Register(UserRegistration register);
        UserRegistration Login(UserLogin login);
    }
}
