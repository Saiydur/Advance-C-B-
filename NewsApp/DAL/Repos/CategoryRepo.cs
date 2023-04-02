using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class CategoryRepo
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepo()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return this.dbContext.Categories.ToList();
        }

        public Category GetCategoryById(Guid id)
        {
            return this.dbContext.Categories.Find(id);
        }

        public void AddCategory(Category category)
        {
            this.dbContext.Categories.Add(category);
            this.dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            this.dbContext.Entry(category).State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            var category = this.dbContext.Categories.Find(id);
            this.dbContext.Categories.Remove(category);
            this.dbContext.SaveChanges();
        }
    }
}
