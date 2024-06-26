using CommandsService.EventProcessing;
using CommandsService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextServices(builder.Configuration,builder.Environment);

builder.Services.AddMapperServices();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddBackgroundServices(builder.Configuration);

builder.Services.AddRepositoryServices();
builder.Services.AddGrpcClientServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapApiEndpoints();

app.PrepPopulation();

app.Run();