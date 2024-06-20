using Microsoft.AspNetCore.Connections;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using ModelLayer.Model;

namespace RepositoryLayer.Helper
{
    public class Consumer
    {
        public static void ConsumMail()
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var chanale = connection.CreateModel();

            //create consumer to read massage from RabbitMQ Queue
            var consumer = new EventingBasicConsumer(chanale);
            //Recived is event to definn to handle incomming massage
            //+= wha=en recive event is called than execute this lambda function 
            //m -it represent the sender
            //arg basic deliver event argument and the body of massage extrated 
            consumer.Received += (m, arg) =>
            {
                var body = arg.Body.ToArray();

                var massage = Encoding.UTF8.GetString(body);
                var dMassage = JsonSerializer.Deserialize<ProducerSentModel>(massage);
                EmailSender.sendMail(dMassage.email,dMassage.massage);
            };
            chanale.BasicConsume(queue: "AdminQueue", autoAck: true, consumer: consumer);



        }
    }
}
