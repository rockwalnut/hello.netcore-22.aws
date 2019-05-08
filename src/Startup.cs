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

            //for integrate overide
            /*  services.AddSingleton<IEnumerable<Post>>(new Post[] {
                new Post { 
                            Id = "1",
                            Author = "Carl-Hugo Marcotte",
                            Title = "Design Patterns",
                            Content = "In this article series, I’d like to go back a little (from my previous Microservices Aggregation article which was more advanced) and        introduce some more basic design patterns. Those patterns help decouple the application flow and extract its responsibilities into separate classes.",
                            Url =  "https://www.forevolve.com/en/articles/2017/08/11/design-patterns-web-api-service-and-repository-part-1/",
                            
                            Published = DateTime.Now.AddDays(1)
                         },

                new Post { 
                            Id = "2",
                            Author = "Clermont-Fd Area, France",
                            Title = "From STUPID to SOLID Code!",
                            Content = "In the following, I will introduce both STUPID and SOLID principles. Keep in mind that these are principles, not laws. However, considering them as laws would be good for those who want to improve themselves.",
                            Url =  "https://williamdurand.fr/2013/07/30/from-stupid-to-solid-code/",
                           
                            Published = DateTime.Now.AddDays(1)
                         },
                new Post { 
                            Id = "3",
                            Author = "Clermont-Fd Area, France",
                            Title = "Dependency injection in ASP.NET Core",
                            Content = "ASP.NET Core supports the dependency injection (DI) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.For more information specific to dependency injection within MVC controllers, see Dependency injection ",
                            Url =  "https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?ranMID=24542&ranEAID=je6NUbpObpQ&ranSiteID=je6NUbpObpQ-jZSnHSmXYcmtlVZJJ83qow&epi=je6NUbpObpQ-jZSnHSmXYcmtlVZJJ83qow&irgwc=1&OCID=AID681541_aff_7593_1243925&tduid=(ir__niyobvxxk0kfr3rexmlij6lydu2xmxpx9eejh2ca00)(7593)(1243925)(je6NUbpObpQ-jZSnHSmXYcmtlVZJJ83qow)()&irclickid=_niyobvxxk0kfr3rexmlij6lydu2xmxpx9eejh2ca00&view=aspnetcore-2.2",
                            
                            Published = DateTime.Now.AddDays(1)
                         }
            }); */


            //dependencies graph
            services.TryAddScoped<IBlogService, BlogService>();
            services.TryAddScoped<IBlogRepository, BlogRepository>();   
            
            services.TryAddScoped<IPostService, PostService>();
            services.TryAddScoped<IPostRepository, PostRepository>();    

           // services.TryAddSingleton<DbContext, NorthwindContext>(); 


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
