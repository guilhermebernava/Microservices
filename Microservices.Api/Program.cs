using Microservices.CrossCutting.Infra;
using Microservices.CrossCutting.Application;
using Microservices.CrossCutting.Api;
using Microservices.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAppDbContext(builder.Configuration["db"]!);
builder.Services.AddServices();
builder.Services.AddValidations();
builder.Services.AddJwt(builder.Configuration["JWTSecretKey"]!);
builder.Services.AddSwagger(builder.Configuration["ApiName"]!);
builder.Services.AddRepositories();

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

