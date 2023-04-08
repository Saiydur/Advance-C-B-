using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAppBlog.Models;
namespace NewsAppBlog
{
    public class RabbitMQService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            _factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: "category_queue", basicProperties: null, body: body);
        }

        public List<Category> GetCategories()
        {
            _channel.QueueDeclare(queue: "category_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            var categories = new List<Category>();

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var responseJson = Encoding.UTF8.GetString(body);
                var receivedCategories = JsonConvert.DeserializeObject<List<Category>>(responseJson);
                categories.AddRange(receivedCategories);
            };

            _channel.BasicConsume(queue: "category_queue", autoAck: true, consumer: consumer);

            while (categories.Count == 0)
            {
                Task.Delay(100).Wait();
            }

            return categories;
        }
    }
}
