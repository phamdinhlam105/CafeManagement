using CafeManagement.Data;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Mappers;
using CafeManagement.Models;
using CafeManagement.Services.Login;
using CafeManagement.Services.PromotionService;
using CafeManagement.Services.Report;
using CafeManagement.Services.StockService;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;
using CafeManagement.Helpers;
using Microsoft.AspNetCore.Http.Json;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Factory;
using CafeManagement.Observers.Subjects;
using CafeManagement.Models.Stock;
using CafeManagement.Factories.Observers;
using CafeManagement.Models.OrderModel;
using CafeManagement.Services.ProductService;
using CafeManagement.Interfaces.Services.ProductService;
using CafeManagement.Interfaces.Services.OrderService;
using CafeManagement.Services.OrderService;
using CafeManagement.Events.Subjects;
using CafeManagement.Interfaces.Facade.StockFacade;
using CafeManagement.Facades;
using CafeManagement.Events.Obsersvers.FinishOrderObs;
using CafeManagement.Events.Obsersvers.StockAdjustmentObs;
using CafeManagement.Events.Obsersvers.StockImportObs;

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
#region Service
//product
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
//order
builder.Services.AddScoped<INewOrderService, NewOrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
//user & token
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//stock
builder.Services.AddScoped<IStockEntryService, StockEntryService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IStockUsageService, StockUsageService>();
builder.Services.AddScoped<IStockQueryService, StockQueryService>();
builder.Services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();
builder.Services.AddScoped<IStockFIFOService, StockFIFOService>();
//report
builder.Services.AddScoped<IReportRetrievalService, ReportRetrievalService>();
builder.Services.AddScoped<IOrderReportUpdateService, OrderReportUpdateService>();
builder.Services.AddScoped<IStockReportUpdateService, StockReportUpdateService>();
builder.Services.AddScoped<IReportCreationService, ReportCreationService>();
//promotion
builder.Services.AddScoped<IPromotionService, PromotionService>();
#endregion

#region Facade
builder.Services.AddScoped<IStockQueryUseCase, StockFacade>();
builder.Services.AddScoped<IStockUpdateUseCase, StockFacade>();
#endregion

#region Observer
builder.Services.AddScoped<IAppObserver<Order>, CustomerUpdater>();
builder.Services.AddScoped<IAppObserver<Order>, OrderReportUpdater>();
builder.Services.AddScoped<IAppObserver<StockEntry>, StockReportByEntryUpdater>();
builder.Services.AddScoped<IAppObserver<StockAdjustment>, EntryByAdjustmentUpdater>();
builder.Services.AddScoped<IAppObserver<StockAdjustment>, StockReportByAdjustmentUpdater>();

builder.Services.AddScoped<ISubject<Order>, OrderCompleteEvent>();
builder.Services.AddScoped<ISubject<StockEntry>, StockImportEvent>();
builder.Services.AddScoped<ISubject<StockAdjustment>, StockAdjustmentEvent>();
#endregion

#region Factory
builder.Services.AddScoped<IObserverFactory<Order>, OrderObsFactory>();
builder.Services.AddScoped<IObserverFactory<StockAdjustment>, AdjustmentObsFactory>();
#endregion
//mapper
#region Mapper
builder.Services.AddScoped<ICustomerMapper, CustomerMapper>();
builder.Services.AddScoped<ICategoryMapper, CategoryMapper>();
builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddScoped<INewOrderMapper, NewOrderMapper>();
builder.Services.AddScoped<IOrderDetailMapper, OrderDetailMapper>();
builder.Services.AddScoped<IStockMapper, StockMapper>();
builder.Services.AddScoped<IStockEntryMapper, StockEntryMapper>();
builder.Services.AddScoped<IStockEntryDetailMapper, StockEntryDetailMapper>();
builder.Services.AddScoped<IReportMapper,ReportMapper>();
builder.Services.AddScoped<IRecipeMapper, RecipeMapper>();
builder.Services.AddScoped<IRecipeDetailMapper, RecipeDetailMapper>();
#endregion

#region CORS
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
#endregion
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
#region Authentication
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
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
    options.RequireHttpsMetadata = true;
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
#endregion

#region Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("NotCustomer", policy =>
        policy.RequireRole(Role.Admin, Role.Manager, Role.Employee)); 
});
#endregion
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
