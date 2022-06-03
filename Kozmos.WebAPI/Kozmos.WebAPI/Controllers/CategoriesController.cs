using Kozmos.WebAPI.Data.Services;
using Kozmos.WebAPI.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kozmos.WebAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoryServices _categoryServices;
        public CategoriesController(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public IActionResult getAllCategoriesController()
        {
            List<CategoryVM> list = _categoryServices.getAllCategoriesService();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult getCategoryByIdController(int id)
        {
            var category = _categoryServices.getCategoryByIdService(id);
            return Ok(category);

        }

        [HttpPost]
        [Authorize]
        public IActionResult createCategoryController(CategoryAddVM cat)
        {
            if (cat == null)
            {
                return BadRequest();
            }

            _categoryServices.addCategoryService(cat);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult deleteCategoryController(int id)
        {
            _categoryServices.deleteCategoryService(id);
            return Ok();
        }
    }
}
