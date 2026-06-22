using FluentValidation;
using InThePocket.Assignment.GardenManager.Ports;
using InThePocket.Assignment.GardenManager.Ports.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddValidatorsFromAssembly(typeof(Program).Assembly);
services.AddControllers();

services.RegisterDependencies();

services.AddDbContext<GardenManagerContext>
(options => options.UseSqlServer(builder.Configuration.GetConnectionString("localDb")));
/*builder.Services.AddDbContext<GardenManagerContext>
(options => options.UseInMemoryDatabase("GardenManagerDb"));*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();