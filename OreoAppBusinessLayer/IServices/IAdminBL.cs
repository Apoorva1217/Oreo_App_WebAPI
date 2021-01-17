using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBusinessLayer.IServices
{
    public interface IAdminBL
    {
        AdminRegistration AdminLogin(AdminLogin login);
        bool RegisterAdmin(AdminRegistration admin);
    }
}
