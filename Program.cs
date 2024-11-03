using Microsoft.EntityFrameworkCore;
using MSMS.Data;
using MSMS.Data.Repos;
using MSMS.Models.MedicineInventory;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseInMemoryDatabase("MSMS").EnableSensitiveDataLogging());

builder.Services.AddScoped<IUserDatabaseRepository, UserRepository>();
builder.Services.AddScoped<IMedicineDatabaseRepository, MedicineRepository>();
builder.Services.AddScoped<IDatabaseRepository<Supplier>, SupplierRepository>();
builder.Services.AddScoped<IPatientDatabaseRepository, PatientRepository>();
builder.Services.AddScoped<IPaymentDatabaseRepository, PaymentRepository>();

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
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
{
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }


    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
