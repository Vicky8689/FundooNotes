using Microsoft.AspNetCore.Connections;
using ModelLayer.Model;
using RabbitMQ.Client;
using RepositoryLayer.Helper;
using System.Text;
using System.Text.Json;

namespace FundooNotes.ProducerFolder
{
    public class Producer
    {
        public static void SentMailProducer(string email ,string massage)
        {

            ProducerSentModel massageModel = new ProducerSentModel() {email=email,massage=massage };
            var serializeMassage = JsonSerializer.Serialize(massageModel);
            //connection Factory Was created and configur RabitMQ server which is on local server
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",

            };
            //satablised connection to RabbitMQ server which is configured
            var connection = factory.CreateConnection();

            //chanale is created to comunicate to RabitMQ server
            var chanal = connection.CreateModel();


            var bodyMassage = Encoding.UTF8.GetBytes(serializeMassage);
            chanal.BasicPublish(exchange: "OrderEx", routingKey: "", basicProperties: null,body: bodyMassage);
            Consumer.ConsumMail();
        }
    }
}
