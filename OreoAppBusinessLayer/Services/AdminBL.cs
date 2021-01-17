using OreoAppBusinessLayer.IServices;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminRegistration AdminLogin(AdminLogin login)
        {
            try
            {
                return this.adminRL.AdminLogin(login);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RegisterAdmin(AdminRegistration register)
        {
            try
            {
                return this.adminRL.RegisterAdmin(register);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
