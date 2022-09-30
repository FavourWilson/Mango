using mango.Web.Service.IService;
using Mango.Services.Models;
using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if(response != null && response.IsSuccess)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
#pragma warning restore CS8604 // Possible null reference argument.
            }
            return View(list);
        }
    }
}
