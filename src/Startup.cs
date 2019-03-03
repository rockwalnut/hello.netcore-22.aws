using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

//DI
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

//model
using hello.netcore_22.aws.Models;
using hello.netcore_22.aws.Services;
using hello.netcore_22.aws.Repositories;

namespace hello.netcore_22.aws
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the 
        //container.
        public void ConfigureServices(IServiceCollection services)
        {
            //in memory
            services.AddDbContext<NorthwindContext>(opt =>
                opt.UseInMemoryDatabase("Northwind"));

            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogRepository, BlogRepository>();    
            services.AddSingleton<NorthwindContext>(); 

            //parameterize
            /*services.AddTransient<NorthwindContext>(provider =>
            {
                //resolve another classes from DI
                var anyOtherClass = provider.GetService<AnyOtherClass>();

                //pass any parameters
                return new NorthwindContext(foo, bar);
            });*/

            //services.TryAddSingleton<NorthwindContext>();

            //services.AddScoped<IBlogService, BlogService>();

            //enable CORES
            services.AddCors(o => o.AddPolicy("AllowCores", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            // Add application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();

            // ...
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseCors("AllowCores");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for 
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //for dependency injection service
            app.ApplicationServices.GetService<IDisposable>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
