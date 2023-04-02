using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class NewsRepo
    {
        private readonly ApplicationDbContext dbContext;

        public NewsRepo()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public IEnumerable<News> GetAllNews()
        {
            return this.dbContext.News.ToList();
        }

        public News GetNewsById(Guid id)
        {
            return this.dbContext.News.Find(id);
        }

        public void AddNews(News news)
        {
            this.dbContext.News.Add(news);
            this.dbContext.SaveChanges();
        }

        public void UpdateNews(News news)
        {
            this.dbContext.Entry(news).State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }

        public void DeleteNews(Guid id)
        {
            var news = this.dbContext.News.Find(id);
            this.dbContext.News.Remove(news);
            this.dbContext.SaveChanges();
        }
    }
}
