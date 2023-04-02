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
    public class NewsService
    {
        private readonly NewsRepo newsRepo;

        public NewsService()
        {
            this.newsRepo = new NewsRepo();
        }

        public IEnumerable<NewsDTO> GetAllNews()
        {
            var newsList = this.newsRepo.GetAllNews();
            var newsDTOs = newsList.Select(n => new NewsDTO
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Date = n.Date,
                Category = new CategoryDTO
                {
                    Id = n.Category.Id,
                    Name = n.Category.Name
                }
            });
            return newsDTOs;
        }

        public NewsDTO GetNewsById(Guid id)
        {
            var news = this.newsRepo.GetNewsById(id);
            var newsDTO = new NewsDTO
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                Date = news.Date,
                Category = new CategoryDTO
                {
                    Id = news.Category.Id,
                    Name = news.Category.Name
                }
            };
            return newsDTO;
        }

        public void AddNews(NewsDTO newsDTO)
        {
            var news = new News
            {
                Id = Guid.NewGuid(),
                Title = newsDTO.Title,
                Description = newsDTO.Description,
                Date = newsDTO.Date,
                CategoryId = newsDTO.Category.Id
            };
            this.newsRepo.AddNews(news);
        }

        public void UpdateNews(NewsDTO newsDTO)
        {
            var news = new News
            {
                Id = newsDTO.Id,
                Title = newsDTO.Title,
                Description = newsDTO.Description,
                Date = newsDTO.Date,
                CategoryId = newsDTO.Category.Id
            };
            this.newsRepo.UpdateNews(news);
        }

        public void DeleteNews(Guid id)
        {
            this.newsRepo.DeleteNews(id);
        }
    }
}
