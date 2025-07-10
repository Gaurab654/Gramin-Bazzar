using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
namespace Gramin_Bazzar_marketplace_for_rural_Nepal_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("GraminDBContextConnection") ?? throw new InvalidOperationException("Connection string 'GraminDBContextConnection' not found.");

            builder.Services.AddDbContext<GraminDBContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<GraminDBContext>();

            // Add services to the container.

            //add session
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();


            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseSession();

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
            app.MapRazorPages();
            app.Run();
        }
    }
}
