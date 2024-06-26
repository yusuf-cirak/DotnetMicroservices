using PlatformService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddMappers();

builder.Services.AddClientServices();
builder.Services.AddDbContextServices(builder.Configuration,builder.Environment);
builder.Services.AddRepositoryServices();


builder.AddGrpcServerServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapApiEndpoints();
app.MapGrpcServerServices();

app.PrepPopulation();

System.Console.WriteLine("Starting the application...");
app.Run();