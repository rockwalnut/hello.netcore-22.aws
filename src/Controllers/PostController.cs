using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hello.netcore_22.aws.Models;
using Microsoft.AspNetCore.Http;
//using ForEvolve.Post.Samples.NinjaApi.Services;

//service
using hello.netcore_22.aws.Services;
using Microsoft.AspNetCore.Cors;
using hello.netcore_22.aws.Exceptions;

namespace hello.netcore_22.aws.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
       // private readonly NorthwindContext _context;

        public PostController(IPostService postService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
           // _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        [EnableCors("AllowCores")]
        [Route("listbyblog")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListByBlogAsync(string blogid)
        {
            var posts = await _postService.ListByBlogAsync(blogid);
            return Ok(posts);
        }

        [EnableCors("AllowCores")]
        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), StatusCodes.Status200OK)]
        public async Task<IActionResult> List()
        {
            var posts = await _postService.ListAsync();
            return Ok(posts);
        }

    }
}