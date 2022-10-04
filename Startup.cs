using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniqueTodoApplication.BackgroundConfiguration;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.MailFolder;
using UniqueTodoApplication.Settings;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Implementation.Repositries;
using UniqueTodoApplication.Implementation.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using UniqueTodoApplication.BackgroundTask;

namespace UniqueTodoApplication
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
            services.AddDbContext<ApplicationContext>(options => options.UseMySQL(Configuration.GetConnectionString("ApplicationContext")));
            services.AddTransient<IMailService, MailService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.Configure<ReminderMailsConfig>(Configuration.GetSection("ReminderMailsConfig"));
            


            services.AddControllersWithViews();
            
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemService, ItemService>();

            services.AddScoped<ITodoitemRepository, TodoitemRepository>();
            services.AddScoped<ITodoitemService, TodoitemService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            
            services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();

            services.AddScoped<IReminderRepository, ReminderRepository>();

            services.Configure<ReminderMailsConfig>(Configuration.GetSection("ReminderMailsConfig"));
            // services.Configure<SkippedMailsConfig>(Configuration.GetSection("SkippedMailsConfig"));
            // services.Configure<AchievementMailsConfig>(Configuration.GetSection("AchievementMailsConfig"));
            // services.Configure<NotificationMailsConfig>(Configuration.GetSection("NotificationMailsConfig"));

            
           services.AddHostedService<ReminderMails>();
          // services.AddHostedService<SkippedMails>();
            // services.AddHostedService<AchievementMails>();
            // services.AddHostedService<NotificationMails>();
            
           services.AddAuthentication();
           services.AddAuthorization();
           services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(config => 
           {
               config.LoginPath = "/User/SignIn";
               config.Cookie.Name = "UniqueApp";
               config.LogoutPath = "/User/SignOut";
           });
            services.AddAuthorization();
            
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
