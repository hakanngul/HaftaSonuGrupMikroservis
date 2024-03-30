// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using Producer.App.Events;
using RabbitMQ.Client;

Console.WriteLine("Producer");


var connectionFactory = new ConnectionFactory
{
    Uri = new Uri("amqps://ecxqqwie:j3VImIVDOcz5p4KBYK4Z_6LECMkLkSor@moose.rmq.cloudamqp.com/ecxqqwie")
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

channel.BasicReturn += (sender, eventArgs) => { Console.WriteLine("Message was returned"); };


// create exchange
channel.ExchangeDeclare("fanout-exchange", ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);


var message = new UserCreatedEvent() { Email = "ahmet@outlook.com", Id = 1 };
var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
channel.BasicPublish("fanout-exchange", "", true, null, body);

Console.WriteLine($"Mesaj gönderildi :{message.Id}");
Console.ReadLine();


//channel.QueueDeclare("demo-queue2", durable: true, exclusive: false, autoDelete: false, arguments: null);


//channel.ConfirmSelect();


//channel.BasicAcks += (sender, eventArgs) => { Console.WriteLine("Message was delivered"); };

//channel.BasicNacks += (sender, eventArgs) => { Console.WriteLine("Message was not delivered"); };


//var properties = channel.CreateBasicProperties();

//properties.Persistent = true;
// ack =>

//ack=false
//int i = 0;
//Enumerable.Range(1, 20).ToList().ForEach(x =>
//{
//    i++;

//    var message = new UserCreatedEvent() { Email = "ahmet@outlook.com", Id = x };
//    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
//    channel.BasicPublish(string.Empty, "demo-queue2", null, body);
//    Console.WriteLine($"Mesaj gönderildi :{message.Id}");
//    channel.WaitForConfirms(TimeSpan.FromSeconds(5));
//    //if (i % 5 == 0)
//    //{
//    //    channel.WaitForConfirms();
//    //}
//});

//Console.ReadLine();
//try
//{
//}
//catch (Exception e)
//{
//    Console.WriteLine(e);
//    throw;
//}