using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MSMS.Data;
using MSMS.Data.Repos;
using MSMS.Models.MedicineInventory;
using MSMS.Services;

var builder = WebApplication.CreateBuilder(args);

var labConnString = builder.Configuration.GetConnectionString("SQLServerJaveDb")!;
var emsConnString = builder.Configuration.GetConnectionString("EMSdb");
var localSqlServerConnString = builder.Configuration.GetConnectionString("LocalSQLServerDb");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

//var dbServerVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<DatabaseContext>(
    //options => options.UseInMemoryDatabase("MSMS").EnableSensitiveDataLogging()
    //options => options.UseMySql(connectionString, dbServerVersion).EnableSensitiveDataLogging()
    options => options.UseSqlServer(localSqlServerConnString).EnableSensitiveDataLogging()
    );

builder.Services.AddDbContext<EMSDatabaseContext>(
        options => options.UseSqlServer().EnableSensitiveDataLogging()
    );

builder.Services.AddScoped<IUserDatabaseRepository, UserRepository>();
builder.Services.AddScoped<IMedicineDatabaseRepository, MedicineRepository>();
builder.Services.AddScoped<IDatabaseRepository<Supplier>, SupplierRepository>();
builder.Services.AddScoped<IPatientDatabaseRepository, PatientRepository>();
builder.Services.AddScoped<IProcedureDatabaseRepository, ProcedureRepository>();
builder.Services.AddScoped<INotificationDatabaseRepository, NotificationRepository>();
builder.Services.AddScoped<IActiveProcedureDatabaseRepository, ActiveProcedureRepository>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Dashboard/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Staff", policy => policy.RequireAssertion(context => context.User.IsInRole("Admin") || context.User.IsInRole("Staff")));
    options.AddPolicy("Doctor", policy => policy.RequireAssertion(context => context.User.IsInRole("Admin") || context.User.IsInRole("Doctor")));



});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DatabaseContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }


    app.UseExceptionHandler("/Login/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
