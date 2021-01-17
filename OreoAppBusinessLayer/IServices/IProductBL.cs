using OreoAppCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBusinessLayer.IServices
{
    public interface IProductBL
    {
        List<Product> GetAllProducts();
    }
}
