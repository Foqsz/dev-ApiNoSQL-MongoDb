using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ProjetoAPI_Treinammento.Data;
using ProjetoAPI_Treinammento.Service;

var builder = WebApplication.CreateBuilder(args);

// Adicionando configuração para MongoDB
//var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
//var mongoClient = new MongoClient(mongoConnectionString);
//builder.Services.AddSingleton<IMongoClient>(mongoClient);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<ProductDbSettings>
    (builder.Configuration.GetSection("FoqsDataBase"));

builder.Services.AddSingleton<ProductService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
