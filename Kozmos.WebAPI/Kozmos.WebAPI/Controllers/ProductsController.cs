using Kozmos.WebAPI.Data.Model;
using Kozmos.WebAPI.Data.Services;
using Kozmos.WebAPI.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kozmos.WebAPI.Controllers
{
    [EnableCors]
    
    [Route("api/[controller]")]
    [ApiController]
    


    public class ProductsController:ControllerBase
    {
        public ProductsServices _productsServices;
        public ProductsController(ProductsServices productsService)
        {
            _productsServices = productsService;
        }

        [HttpGet]
        
        public IActionResult GetAllProductsController()
        {
            List<ProductWithCategoryNameVM> list =_productsServices.getAllProductsService();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductByIdController(int id)
        {
            var prod = _productsServices.getProductByIdService(id);
            return Ok(prod);


        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateProductController(ProductVM prod)
        {
            _productsServices.createProductService(prod);
            return Ok();
        }
        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult DeleteProductController(int id)
        {
            _productsServices.deleteProductService(id);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(UpdateProductViewModel vm, int id)
        {
            bool result = await _productsServices.updateProduct(id, vm);
            if(result)
            {
                return Ok("Güncelleme işlemi başarılı");
            }
            return NotFound("Güncelleme işlemi başarısız, kayıt bulunamadı");
        }

        [Authorize]
        [HttpPut("changefeatured/{id}")]
        public async Task<ActionResult> ChangeFeaturedController(int id)
        {
            bool result = await _productsServices.ChangeFeaturedStateService(id);
            if (result)
            {
                return Ok("Öne çıkarma durumu başarıyla değiştirildi");
            }
            return BadRequest("İşlem gerçekleştirilemedi");
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchProductByNameController([FromQuery] string input)
        {
            var searchedList = await _productsServices.SearchProductByNameService(input);
            if (searchedList.Count > 0)
            {
                return Ok(searchedList);
            }
            else
            {
                return BadRequest("Böyle bir ürün bulunmamaktadır.");
            }
        }


    }
}
