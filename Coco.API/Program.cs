using Coco.Core.Interfaces;
using Coco.Infraestructure.Persistence;
using Coco.Infraestructure.Repositories;
using Coco.Services.Interfaces;
using Coco.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

//Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IStoreRepository, StoreRepository>();

//Services
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IStoreService, StoreService>();



builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Coco"))
                );

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
