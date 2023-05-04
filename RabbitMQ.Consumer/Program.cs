using System.Text;
using System.Text.Json.Serialization;
using AndreTurismoApp.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string QUEUE_NAME = "AddressesProducer";

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    while (true)
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var returnAddress = Encoding.UTF8.GetString(body);
                            var message = JsonConvert.DeserializeObject<Address>(returnAddress);
                        };
                    }
                }
            }
        }
    }
}