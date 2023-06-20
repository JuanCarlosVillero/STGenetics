using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using STGenetics.Application.Abstraction.Queries;
using STGenetics.Application.Abstraction.Services;
using STGenetics.ApplicationServices;
using STGenetics.Domain.Abstraction;
using STGenetics.DomainServices;
using STGenetics.DomainServices.Abstraction.Repository;
using STGenetics.Infrastructure.Queries;
using STGenetics.Repositories.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IAnimalApplicationServices, AnimalApplicationServices>();
builder.Services.AddTransient<IOrderApplicationServices, OrderApplicationServices>();
builder.Services.AddTransient<IAnimalDomainServices, AnimalDomainServices>();
builder.Services.AddTransient<IOrderDomainServices, OrderDomainServices>();
builder.Services.AddTransient<IAnimalQueries>(s => new AnimalQueries(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAnimalRepository>(s => new AnimalRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IOrderLineRepository>(s => new OrderLineRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IOrderRepository>(serviceProvider => 
    new OrderRepository(
        connString: builder.Configuration.GetConnectionString("DefaultConnection"),
        orderLineRepository: serviceProvider.GetRequiredService<IOrderLineRepository>()
    )
);


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
            };
        }
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwtToken_Auth_API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Here enter JWT Token with bearer format like bearer[space] token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            //new List<string>()
            new string[]{}
        }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
