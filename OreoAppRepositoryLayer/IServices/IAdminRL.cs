using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppRepositoryLayer.IServices
{
    public interface IAdminRL
    {
        bool RegisterAdmin(AdminRegistration register);
        AdminRegistration AdminLogin(AdminLogin login);
    }
}
