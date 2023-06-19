using Microsoft.Extensions.DependencyInjection;
using STGenetics.Application.Abstraction.Queries;
using STGenetics.Application.Abstraction.Services;
using STGenetics.ApplicationServices;
using STGenetics.Domain.Abstraction;
using STGenetics.DomainServices;
using STGenetics.DomainServices.Abstraction.Repository;
using STGenetics.Infrastructure.Queries;
using STGenetics.Repositories.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IAnimalApplicationServices, AnimalApplicationServices>();
builder.Services.AddTransient<IAnimalDomainServices, AnimalDomainServices>();
builder.Services.AddTransient<IAnimalQueries>(s => new AnimalQueries(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAnimalRepository>(s => new AnimalRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

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
