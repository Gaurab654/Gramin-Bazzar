using Microsoft.EntityFrameworkCore;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Services;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("GraminDBContextConnection")
                ?? throw new InvalidOperationException("Connection string 'GraminDBContextConnection' not found.");

            // Add DbContext
            builder.Services.AddDbContext<GraminDBContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<RecommendationService>();//recentlya


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //  Add Identity with custom ApplicationUser
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() // Adds Role support
                .AddEntityFrameworkStores<GraminDBContext>()
                .AddDefaultTokenProviders();

            // 3Add session and MVC
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // 4️⃣ Middleware order is important
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // ✅ Must come before Authorization
            app.UseAuthorization();

            app.UseSession();

            // 5️⃣ Map routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
