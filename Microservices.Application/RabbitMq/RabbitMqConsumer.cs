
using Microservices.Application.Helpers;
using Microservices.Domain.Entities;
using Microservices.Domain.Repositories;
using Microservices.Infra.Data;
using Microservices.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Microservices.Application.RabbitMq;
public class RabbitMqConsumer : IHostedService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchangeName;
    private readonly string _routingKey;
    private readonly string _queueName;
    private readonly IRepository<Recomendation> _recomendationRepository;

    public RabbitMqConsumer(IConfiguration configuration, AppDbContext db)
    {

        var connectionFactory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQConnection:HostName"],
            Port = int.Parse(configuration["RabbitMQConnection:Port"]!),
            UserName = configuration["RabbitMQConnection:UserName"],
            Password = configuration["RabbitMQConnection:Password"],
            VirtualHost = configuration["RabbitMQConnection:VirtualHost"]
        };

        _routingKey = configuration["RoutingKey"]!;
        _exchangeName = configuration["ExchangeName"]!;
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
        _queueName = _channel.QueueDeclare(queue: _routingKey, exclusive: false, autoDelete: false).QueueName;
        _channel.QueueBind(queue: _routingKey, exchange: _exchangeName, routingKey: _routingKey);
        _channel.BasicQos(0, 1, false);
        _recomendationRepository = new Repository<Recomendation>(db);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ConsoleHelper.ShowInformation($"Message Received - {message}");

                var entity = JsonSerializer.Deserialize<Recomendation>(message);
                if (entity == null) throw new InvalidCastException("Error in deserialize data from QUEUE");

                await _recomendationRepository.CreateAsync(entity);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            ConsoleHelper.ShowError($"Error to consume message from QUEUE - {ex.Message}");
            return Task.CompletedTask;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        return Task.CompletedTask;
    }
}
