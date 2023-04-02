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
    public class NewsController : ApiController
    {
        private readonly NewsService newsService;

        public NewsController()
        {
            this.newsService = new NewsService();
        }

        [HttpGet]
        [Route("api/news/all")]
        public IHttpActionResult GetAllNews()
        {
            var news = this.newsService.GetAllNews();
            return Ok(news);
        }

        [HttpGet]
        public IHttpActionResult GetNewsById(Guid id)
        {
            var news = this.newsService.GetNewsById(id);
            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }

        [HttpPost]
        public IHttpActionResult AddNews([FromBody] NewsInputModel newsInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsDTO = new NewsDTO
            {
                Title = newsInput.Title,
                Description = newsInput.Description,
                Date = newsInput.Date,
                CategoryId = newsInput.CategoryId
            };
            this.newsService.AddNews(newsDTO);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateNews(Guid id, [FromBody] NewsInputModel newsInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsDTO = new NewsDTO
            {
                Id = id,
                Title = newsInput.Title,
                Description = newsInput.Description,
                Date = newsInput.Date,
                CategoryId = newsInput.CategoryId
            };
            this.newsService.UpdateNews(newsDTO);

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteNews(Guid id)
        {
            this.newsService.DeleteNews(id);
            return Ok();
        }
    }
}
