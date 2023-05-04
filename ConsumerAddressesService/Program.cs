using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using AndreTurismoApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AndreTurismoApp.Models;
using System.Drawing.Text;

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
                            var address = JsonConvert.DeserializeObject<Address>(returnAddress);
                            using (var httpClient = new HttpClient())
                            {
                                var response = httpClient.PostAsJsonAsync("http://localhost:7054/api/Addresses", address).Result;
                                if (!response.IsSuccessStatusCode)
                                {
                                    Console.WriteLine("Failed to save address: " + response.ReasonPhrase);
                                }
                            }
                        };

                        channel.BasicConsume(queue: QUEUE_NAME,
                                             autoAck: true,
                                             consumer: consumer);

                        Thread.Sleep(2000);
                    }
                }
            }
        }
    }
}