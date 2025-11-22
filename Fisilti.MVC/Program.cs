using Application.Mappings;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FisiltiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FisiltiDB"));
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddIdentity<AppUser, AppRole>(opt =>
    {
        //Þifre Kurallarý
        opt.Password.RequireNonAlphanumeric = false;
        //opt.Password.RequireUppercase = false;


        //E-Posta doðrulama zorunlu olsun
        opt.SignIn.RequireConfirmedEmail = true;

        //Lockout Ayarlarý
        opt.Lockout.MaxFailedAccessAttempts = 5; // Hatalý giriþ sayýsý
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); //Kilit Süresi
        opt.Lockout.AllowedForNewUsers = true;

    })
    .AddEntityFrameworkStores<FisiltiDbContext>()
    .AddDefaultTokenProviders();

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
