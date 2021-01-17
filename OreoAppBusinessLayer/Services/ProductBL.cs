using OreoAppBusinessLayer.IServices;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppBusinessLayer.Services
{
    public class ProductBL : IProductBL
    {
        private readonly IProductRL productRL;

        public ProductBL(IProductRL productRL)
        {
            this.productRL = productRL;
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return this.productRL.GetALLProducts();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
