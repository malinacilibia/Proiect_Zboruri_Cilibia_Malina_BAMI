using Microsoft.EntityFrameworkCore;
using Proiect_Zboruri_Cilibia_Malina.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Proiect_Zboruri_Cilibia_Malina.Data.FlightContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ISatisfactionPredictionService, SatisfactionPredictionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:61303");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
