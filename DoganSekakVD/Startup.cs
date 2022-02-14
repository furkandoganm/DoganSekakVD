using AppCore.DataAccess.Configs;
using AppCore.Utils;
using AppCore.Utils.Bases;
using Business.Services;
using Business.Services.Bases;
using DataAccess.Contexts.EntityFramework;
using DataAccess.Repositories;
using DataAccess.Repositories.Bases;
using DoganSekakVD.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DoganSekakVD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    config.SlidingExpiration = true;
                });

            services.AddSession(config =>
            {
                config.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            ConnectionConfig.ConnectionString = Configuration.GetConnectionString("DSContext");
            services.AddScoped<DbContext, DSContext>();
            services.AddScoped<CategoryRepositoryBase, CategoryRepository>();
            services.AddScoped<CityRepositoryBase, CityRepository>();
            services.AddScoped<DistrictRepositoryBase, DistrictRepository>();
            services.AddScoped<PostNumberRepositoryBase, PostNumberRepository>();
            services.AddScoped<ProductRepositoryBase, ProductRepository>();
            services.AddScoped<ProductionFacilityRepositoryBase, ProductionFacilityRepository>();
            services.AddScoped<ProductionFacilityProductRepositoryBase, ProductionFacilityProductRepository>();
            services.AddScoped<ReviewRepositoryBase, ReviewRepository>();
            services.AddScoped<RoleRepositoryBase, RoleRepository>();
            services.AddScoped<UserRepositoryBase, UserRepository>();
            services.AddScoped<UserProductRepositoryBase, UserProductRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IPostNumberService, PostNumberService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductionFacilityService, ProductionFacilityService>();
            services.AddScoped<IProductionFacilityProductService, ProductionFacilityProductService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProductService, UserProductService>();
            services.AddScoped<IAccountService, AccountService>();

            AppSettingsUtilBase appSetingsUtil = new AppSettingsUtil(Configuration);
            appSetingsUtil.BindAppSettings<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
