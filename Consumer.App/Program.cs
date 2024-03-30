// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using Consumer.App;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Consumer");
Console.WriteLine("Mesajlar bekleniyor...");

var connectionFactory = new ConnectionFactory
{
    Uri = new Uri("amqps://ecxqqwie:j3VImIVDOcz5p4KBYK4Z_6LECMkLkSor@moose.rmq.cloudamqp.com/ecxqqwie"),
    DispatchConsumersAsync = true
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

//create queue
channel.QueueDeclare("fanout-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
channel.QueueDeclare("fanout-queue2", durable: true, exclusive: false, autoDelete: false, arguments: null);

// bind queue to exchange
channel.QueueBind("fanout-queue", "fanout-exchange", string.Empty, null);

channel.QueueBind("fanout-queue2", "fanout-exchange", string.Empty, null);


var consumer = new AsyncEventingBasicConsumer(channel);

consumer.Received += ConsumeMethod;

async Task ConsumeMethod(object? sender, BasicDeliverEventArgs eventArgs)
{
    try
    {
        await Task.Delay(1000);
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        var userCreatedEvent = JsonSerializer.Deserialize<UserCreatedEvent>(message);
        Console.WriteLine($"Gelen Mesaj : {userCreatedEvent.Email}");
        channel.BasicAck(eventArgs.DeliveryTag, false);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}


channel.BasicConsume("fanout-queue", false, consumer);


Console.ReadLine();