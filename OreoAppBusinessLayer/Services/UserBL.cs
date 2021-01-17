using OreoAppBusinessLayer.IServices;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserRegistration Login(UserLogin login)
        {
            try
            {
                return this.userRL.Login(login);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RegisterUser(UserRegistration register)
        {
            try
            {
                return this.userRL.Register(register);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
