using serviceMicroservice.Application.Services;
using serviceMicroservice.Domain.Ports;
using serviceMicroservice.Infrastructure.Connection;
using serviceMicroservice.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSql");
var connectionManager = DatabaseConnectionManager.GetInstance(connectionString!);
builder.Services.AddSingleton(connectionManager);


//Inyeccion de Capas
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<IDbConnectionFactory, PostgreSqlConnection>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swash
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
