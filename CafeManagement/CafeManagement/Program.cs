using CafeManagement.Data;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Mappers;
using CafeManagement.Services;
using CafeManagement.Services.PromotionService;
using CafeManagement.Services.Report;
using CafeManagement.Services.Stock;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();
builder.Services.AddScoped<IReportCreationService, ReportCreationService>();
builder.Services.AddScoped<IReportRetrievalService, ReportRetrievalService>();
builder.Services.AddScoped<IYearlyReportService, YearlyReportService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();

//mapper
builder.Services.AddScoped<ICustomerMapper, CustomerMapper>();
builder.Services.AddScoped<ICategoryMapper, CategoryMapper>();
builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddScoped<INewOrderMapper, NewOrderMapper>();
builder.Services.AddScoped<IOrderDetailMapper, OrderDetailMapper>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
