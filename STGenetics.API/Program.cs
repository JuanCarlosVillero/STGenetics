using Microsoft.Extensions.DependencyInjection;
using STGenetics.Application.Abstraction.Queries;
using STGenetics.Application.Abstraction.Services;
using STGenetics.ApplicationServices;
using STGenetics.Infrastructure.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IAnimalApplicationServices, AnimalApplicationServices>();
builder.Services.AddTransient<IAnimalQueries>(s => new AnimalQueries(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
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
