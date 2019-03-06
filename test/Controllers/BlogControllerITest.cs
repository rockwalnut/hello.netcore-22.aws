using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Serilog;

//model
using hello.netcore_22.aws;
using hello.netcore_22.aws.Controllers;
using hello.netcore_22.aws.Models;
using hello.netcore_22.aws.Services;
using hello.netcore_22.aws.test.Utilities;
using Newtonsoft.Json;
using hello.netcore_22.aws.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace hello.netcore_22.aws.test.Controllers
{
    public class BlogControllerITest : BaseHttpTest
    {

        protected Mock<IBlogRepository> mockRepository { get; set; }

        public BlogControllerITest()
        {       
            mockRepository = new Mock<IBlogRepository>();
            
            //serilog
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        }

        public class ListAsync : BlogControllerITest
        {
            private IEnumerable<Blog> Blogs => new Blog[] {
                new Blog { Id= "1", Name = "http://blogs.msdn.com/dotnet", Title = "Dotnet" },
                new Blog { Id= "2", Name = "http://blogs.msdn.com/webdev", Title = "Webdev" },
                new Blog { Id= "3", Name = "http://blogs.msdn.com/visualstudio", Title = "Vscode" }
            };

            protected override void ConfigureServices(IServiceCollection services)
            {
                //IoC
                 services
                .AddSingleton(x => mockRepository.Object);
            }

            [Fact]
            public async Task Should_return_the_list_blogs()
            {
                // Arrange

                var expectedNumberOfBlogs = Blogs.Count();
                mockRepository
                    .Setup(x => x.ListAsync())
                        .ReturnsAsync(Blogs);

                // Act
                var httpResponse = await Client.GetAsync("api/blog/list");

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var blogs = JsonConvert.DeserializeObject<Blog[]>(stringResponse);
    
                //Log.Information(stringResponse);

                Assert.NotNull(blogs);
                Assert.Equal(expectedNumberOfBlogs, blogs.Length);
                Assert.Collection(blogs,
                    clan => Assert.Equal(Blogs.ElementAt(0).Title, blogs[0].Title),
                    clan => Assert.Equal(Blogs.ElementAt(1).Title, blogs[1].Title),
                    clan => Assert.Equal(Blogs.ElementAt(2).Title, blogs[2].Title)
                );
            }
}
    }
}