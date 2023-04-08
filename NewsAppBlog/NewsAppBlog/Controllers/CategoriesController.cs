using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAppBlog.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NewsAppBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly RabbitMQService _rabbitMQService;

        public CategoriesController(DataContext context, RabbitMQService rabbitMQService)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            
            var message = new { cmd = "getCategories" };
            var json = JsonConvert.SerializeObject(message);
            _rabbitMQService.SendMessage(json);

            return categories;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var message = new { cmd = "categoryCreated", category };
            var json = JsonConvert.SerializeObject(message);
            _rabbitMQService.SendMessage(json);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }
    }
}
