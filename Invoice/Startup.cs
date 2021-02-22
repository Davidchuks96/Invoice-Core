using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Invoice.Core.Interfaces;
using Invoice.Core.Interfaces.Base;
using Invoice.Data.AppDataContext;
using Invoice.Data.Repository;
using Invoice.Data.Repository.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Invoice
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                       .AddCookie(options =>
                       {
                           options.LoginPath = "/Home/login/";
                       });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")));

            services.AddSingleton<IConfiguration>(Configuration);

            //services.AddMvc();
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddMvcCore(options =>
            {
                options.RequireHttpsPermanent = true; // does not affect api requests
                options.RespectBrowserAcceptHeader = true; // false by default
            
            })
        //.AddApiExplorer()
        //.AddAuthorization()
        .AddFormatterMappings()
        //.AddCacheTagHelper()
        //.AddDataAnnotations()
        //.AddCors()
        .AddJsonFormatters(); // JSON, or you can build your own custom one (above)

            services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            //var host = Configuration["host"];

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ISaleItemRepository, SaleItemRepository>();
            services.AddTransient<IStoreSettingRepository, StoreSettingRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }


            
           
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
