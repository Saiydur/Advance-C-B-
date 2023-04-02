using BLL.DTOs;
using BLL.Services;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewsApp.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly CategoryService categoryService;

        public CategoryController()
        {
            this.categoryService = new CategoryService();
        }

        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            var categories = this.categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult GetCategoryById(Guid id)
        {
            var category = this.categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IHttpActionResult AddCategory([FromBody] CategoryInputModel categoryInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDTO = new CategoryDTO
            {
                Name = categoryInput.Name
            };
            this.categoryService.AddCategory(categoryDTO);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory(Guid id, [FromBody] CategoryInputModel categoryInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDTO = new CategoryDTO
            {
                Id = id,
                Name = categoryInput.Name
            };
            this.categoryService.UpdateCategory(categoryDTO);

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(Guid id)
        {
            this.categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
