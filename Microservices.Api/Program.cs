using Microservices.Api.Middlewares;
using Microservices.Application.RabbitMq;
using Microservices.CrossCutting.Api;
using Microservices.CrossCutting.Infra;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAppDbContext(builder.Configuration["db"]!);
//builder.Services.AddJwt(builder.Configuration["JWTSecretKey"]!);
builder.Services.AddSwagger(builder.Configuration["ApiName"]!);
builder.Services.AddRepositories();
builder.Services.AddHostedService<RabbitMqConsumer>();



var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

