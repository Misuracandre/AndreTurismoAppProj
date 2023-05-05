using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace AndreTurismoApp.Services.Producers
{
    public class ProducerCitiesService
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "ProducerCitiesService";

        public ProducerCitiesService(ConnectionFactory factory)
        {
            _factory = factory;
        }

        [HttpPost]
        public ActionResult PostMQAddresses([FromBody] City city)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var stringfieldCity = JsonConvert.SerializeObject(city);
                    var bytesCity = Encoding.UTF8.GetBytes(stringfieldCity);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesCity
                        );
                }
            }
            return new StatusCodeResult((int)HttpStatusCode.Accepted);
        }
    }
}
