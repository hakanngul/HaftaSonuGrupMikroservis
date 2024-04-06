using MassTransit;
using MicroserviceTwo.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserCreatedEventConsumer>();

    x.UsingRabbitMq((context, configure) =>
    {
        configure.PrefetchCount = 30;
        configure.Host(builder.Configuration.GetConnectionString("RabbitMq"), x => { });


        configure.ReceiveEndpoint("microservice-two.user.created.event",
            e => { e.ConfigureConsumer<UserCreatedEventConsumer>(context); });
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();