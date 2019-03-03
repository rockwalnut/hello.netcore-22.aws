using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Serilog;
using System;
using Xunit;
using Moq;

//model
using hello.netcore_22.aws;
using hello.netcore_22.aws.Controllers;
using hello.netcore_22.aws.Models;
using hello.netcore_22.aws.Services;


namespace hello.netcore_22.aws.test.Controllers
{
    public class BlogControllerTest
    {
        protected BlogController ControllerUnderTest { get; set; }

        protected Blog[] expectedBlog { get; set; }

        public BlogControllerTest()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        }

        private IEnumerable<Blog> GetBlogTestSessions()
        {
            throw new NotImplementedException();
        }

        public class ListAsync : BlogControllerTest
        {
            
            [Fact]
            public async void Should_return_OkObjectResult_with_blogs()
            {
                // Arrange
                //should serialize from JSON
                expectedBlog = new Blog[]
                {
                    new Blog() { 
                                Id = "b1357cd1-2901-3e8c-9852-1e659bceae98",
                                Title = "Fantastic Beasts: The Crimes of Grindelwald",
                                Name = "JK. Rowling",
                                Description = "Fantastic Beasts: The Crimes of Grindelwald is a 2018 fantasy film directed by David Yates and written by J. K. Rowling. A joint British and American production, it is the sequel to Fantastic Beasts and Where to Find Them (2016). It is the second instalment in the Fantastic Beasts film series, and the tenth overall in the Wizarding World franchise, which began with the Harry Potter film series. The film features an ensemble cast that includes Eddie Redmayne, Katherine Waterston, Dan Fogler, Alison Sudol, Ezra Miller, Zoë Kravitz, Callum Turner, Claudia Kim, William Nadylam, Kevin Guthrie, Jude Law, and Johnny Depp. The plot follows Newt Scamander and Albus Dumbledore as they attempt to take down the dark wizard Gellert Grindelwald, while facing new threats in a more divided wizarding world.",
                                Level = 9, 
                                Created = DateTime.Now
                            },

                    new Blog() {  
                                Id = "a1357cd1-2901-3e8c-9852-1e659bceae51",
                                Title = "Design Patterns: Asp.Net Core Web API",
                                Name = "Microsoft",
                                Description = "In this article series, I’d like to go back a little (from my previous Microservices Aggregation article which was more advanced) and introduce some more basic design patterns. Those patterns help decouple the application flow and extract its responsibilities into separate classes.",
                                Created = DateTime.Now,
                                Level = 5
                            }
                };

                //setup DI
                var mockServ = new Mock<IBlogService>();
                mockServ.Setup(repo => repo.ListAsync()).ReturnsAsync(expectedBlog);

                ControllerUnderTest = new BlogController(mockServ.Object);

                // Act
                var result = await ControllerUnderTest.ListAsync();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsType<Blog[]>(okResult.Value);

                //Log.Information(okResult.Value.GetType().ToString());
                
                Assert.Same(expectedBlog, model);
            }
        }
    }
}