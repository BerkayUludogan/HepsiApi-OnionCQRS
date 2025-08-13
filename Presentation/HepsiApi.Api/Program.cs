using HepsiApi.Persistence;
using HepsiApi.Application;
using HepsiApi.Mapper;
using HepsiApi.Infrastructure;
using HepsiApi.Application.Exceptions;
using Microsoft.OpenApi.Models;
using HepsiApi.Infrastructure.Tokens;
using HepsiApi.Application.Consts;
var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var env = builder.Environment;

builder.Configuration.SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCustomMapper();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hepsi API", Version = "v1", Description = "Hepsi API swagger client" });
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "'Token'i girebilirsiniz. \r\n\r\n Örneðin \"eyJhbGci0iJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            },
            Array.Empty<string>()
        }
    });

});
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
