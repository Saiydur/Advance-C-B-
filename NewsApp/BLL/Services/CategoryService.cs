using BLL.DTOs;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService
    {
        private readonly CategoryRepo categoryRepo;

        public CategoryService()
        {
            this.categoryRepo = new CategoryRepo();
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = this.categoryRepo.GetAllCategories();
            var categoryDTOs = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            });
            return categoryDTOs;
        }

        public CategoryDTO GetCategoryById(Guid id)
        {
            var category = this.categoryRepo.GetCategoryById(id);
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
            return categoryDTO;
        }

        public void AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryDTO.Name
            };
            this.categoryRepo.AddCategory(category);
        }

        public void UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };
            this.categoryRepo.UpdateCategory(category);
        }

        public void DeleteCategory(Guid id)
        {
            this.categoryRepo.DeleteCategory(id);
        }
    }
}
