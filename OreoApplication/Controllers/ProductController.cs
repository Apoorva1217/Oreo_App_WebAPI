using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OreoAppBusinessLayer.IServices;
using OreoAppCommonLayer.Model;

namespace OreoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBL productBL;
        IConfiguration configuration;

        public ProductController(IProductBL productBL, IConfiguration configuration)
        {
            this.productBL = productBL;
            this.configuration = configuration;
        }

        [HttpGet("GetProducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<Product> productList = this.productBL.GetAllProducts();
                if (productList != null)
                {
                    return this.Ok(new { success = true, Message = "Successfully fetched all Products", data = productList });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "Unsuccessful...can't fetch Products" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
    }
}
