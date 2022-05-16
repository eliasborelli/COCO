using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Infraestructure.Persistence;
using Coco.Infraestructure.Repositories;
using Coco.Services.Interfaces;
using Coco.Services.Services;
using Coco.Services.Services.Voucher;
using Coco.Services.Services.Voucher.VoucherStrategy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IStoreRepository, StoreRepository>();

//Services
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddTransient<IVirtualCartService, VirtualCartService>();
builder.Services.AddTransient<IVoucherService, VoucherService>();



builder.Services.AddTransient<Coco.Services.Services.Voucher.IVoucherStrategy, VoucherBetweenCategoriesStrategy>();
builder.Services.AddTransient<Coco.Services.Services.Voucher.IVoucherStrategy, VoucherBetweenDatesStrategy>();
builder.Services.AddTransient<Coco.Services.Services.Voucher.IVoucherStrategy, VoucherPayAndTakeStrategy>();



builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Coco"))
                );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coco.Api", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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

