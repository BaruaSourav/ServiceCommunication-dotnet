using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncCommServices
{
    public class RabbitMQMessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQMessageBusClient(IConfiguration config)
        {
            _config = config;
            // Constructing a connection factory for RabbitMQ message bus
            var factory = new ConnectionFactory(){ 
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])};
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("==> RabbitMQ connection created..");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"==> Connection to RMQ unsuccessful : {ex.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishDto platPublishDto)
        {
            var message = JsonSerializer.Serialize(platPublishDto);
            if(_connection.IsOpen)
            {
                Console.WriteLine("==> RabbitMQ Connection is open, publishing message..");
                PublishMessage(message);
            }
            else
            {
                Console.WriteLine("==> RabbitMQ Connection is not open...");
            }
        }
        /// <summary>
        /// Publishes the message to the message bus
        /// </summary>
        /// <param name="msg">Message to be sent</param>
        private void PublishMessage(string msg)
        {
            var body = Encoding.UTF8.GetBytes(msg);
            _channel.BasicPublish(exchange:"trigger",
                                    routingKey: "",
                                    basicProperties: null,
                                    body : body);
            Console.WriteLine($"==> Published message : {msg} to trigger exchange");
        }

        public void Dispose()
        {
            Console.WriteLine("Message bus disposed");
            if(_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("==> RabbitMQ Connection Shutting down..");
        }
    }
}