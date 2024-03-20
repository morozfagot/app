using Microsoft.EntityFrameworkCore;
using WebApplication10.Repositories;

namespace WebApplication10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TradingDbContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString(nameof(TradingDbContext)));
            });
            builder.Services.AddTransient<ClientRepository>();
            builder.Services.AddTransient<PortfolioRepository>();

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
        }
    }
}
