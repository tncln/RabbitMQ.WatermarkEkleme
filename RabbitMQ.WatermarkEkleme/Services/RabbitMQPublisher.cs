using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQ.WatermarkEkleme.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }
        public void Publish(productimageCreatedEvent productimageCreatedEvent)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(productimageCreatedEvent);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var property = channel.CreateBasicProperties();
            property.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName,routingKey: RabbitMQClientService.RoutingWatermark,basicProperties:property,body:bodyByte);
        }
    }
}
