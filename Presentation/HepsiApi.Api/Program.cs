using HepsiApi.Persistence;
using HepsiApi.Application;
using HepsiApi.Mapper;
using HepsiApi.Application.Exceptions;
var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment;

builder.Configuration.SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCustomMapper();
var app = builder.Build();


// Swagger UI sadece Development ortamýnda açýlýr
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfiggureExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
