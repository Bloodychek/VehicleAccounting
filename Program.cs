using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleAccounting;
using VehicleAccounting.Data;
using VehicleAccounting.Models;
using VehicleAccounting.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnectionToMainDb");
builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(connection));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<IRepository<Application>, ApplicationRepository>();
builder.Services.AddTransient<IRepository<Customer>, CustomerRepository>();
builder.Services.AddTransient<IRepository<Goods>, GoodsRepository>();
builder.Services.AddTransient<IRepository<OrderExecutor>, OrderExecutorRepository>();
builder.Services.AddTransient<IRepository<VehicleAccounting.Models.Route>, RouteRepository>();
builder.Services.AddTransient<IRepository<Transport>, TransportRepository>();
builder.Services.AddTransient<IRepository<Treaty>, TreatyRepository>();
builder.Services.AddTransient<IRepository<TypeOfGoods>, TypeOfGoodsRepository>();
builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var buildServices = builder.Services.BuildServiceProvider();
var service = buildServices.GetService<UserManager<IdentityUser>>();
var role = buildServices.GetService<RoleManager<IdentityRole>>();
await RoleInitializer.InitializeAsync(service, role);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
