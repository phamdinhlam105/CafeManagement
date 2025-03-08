using CafeManagement.Data;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Mappers;
using CafeManagement.Models;
using CafeManagement.Services;
using CafeManagement.Services.Login;
using CafeManagement.Services.Store;
using CafeManagement.Services.PromotionService;
using CafeManagement.Services.Report;
using CafeManagement.Services.Stock;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.   
builder.Services.AddDbContext<CafeManagementDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("CafeManagementConnection"),
   ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CafeManagementConnection"))));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INewOrderService, NewOrderService>();
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
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IExportBillService, ExportBillService>();
builder.Services.AddScoped<IReportUpdateService, ReportUpdateService>();

//mapper
builder.Services.AddScoped<ICustomerMapper, CustomerMapper>();
builder.Services.AddScoped<ICategoryMapper, CategoryMapper>();
builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddScoped<INewOrderMapper, NewOrderMapper>();
builder.Services.AddScoped<IOrderDetailMapper, OrderDetailMapper>();

//CORS
var MyAllowSpecificOrigins = "_myAllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()  // allow all domain
                  .AllowAnyMethod()   // allow all method (GET, POST, PUT, DELETE,...)
                  .AllowAnyHeader();  // allow all headers
        });
});
//authentication
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    // require symbol
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<CafeManagementDbContext>()
    .AddDefaultTokenProviders();
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
app.UseCors(MyAllowSpecificOrigins);

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
