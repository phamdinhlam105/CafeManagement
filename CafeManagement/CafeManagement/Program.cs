using CafeManagement.Data;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Mappers;
using CafeManagement.Services;
using CafeManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CafeManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CafeManagementConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockEntryService, StockEntryService>();

//mapper
builder.Services.AddScoped<ICustomerMapper, CustomerMapper>();
builder.Services.AddScoped<ICategoryMapper, CategoryMapper>();
builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddScoped<INewOrderMapper, NewOrderMapper>();
builder.Services.AddScoped<IOrderDetailMapper, OrderDetailMapper>();
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
