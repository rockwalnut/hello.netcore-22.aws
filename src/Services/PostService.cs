
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hello.netcore_22.aws.Exceptions;

//model
using hello.netcore_22.aws.Models;
using hello.netcore_22.aws.Repositories;

namespace hello.netcore_22.aws.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _PostRepository;
        //private readonly IClanService _clanService;

        public PostService(IPostRepository PostRepository) //, IClanService clanService)
        {
            _PostRepository = PostRepository ?? throw new ArgumentNullException(nameof(PostRepository));
            //_clanService = clanService ?? throw new ArgumentNullException(nameof(clanService));
        }

        public async Task<Post> CreateAsync(Post post)
        {
            //await EnforceClanExistenceAsync(Post.Clan.Name);
            var createdPost = await _PostRepository.CreateAsync(post);
            return createdPost;
        }


        public async Task<Post> UpdateAsync(Post post)
        {
           // await EnforceClanExistenceAsync(Post.Clan.Name);
            //await EnforcePostExistenceAsync(Post.Clan.Name, Post.Key);
            var updatedPost = await _PostRepository.UpdateAsync(post);
            return updatedPost;
        }

        public async Task<Post> DeleteAsync(string id)
        {
           // await EnforcePostExistenceAsync(clanName, PostKey);
            var deletedPost = await _PostRepository.DeleteAsync(id);
            return deletedPost;
        }

        public Task<IEnumerable<Post>> ListAsync()
        {
            return _PostRepository.ListAsync();
        }

        public Task<IEnumerable<Post>> ListByBlogAsync(string id)
        {
            return _PostRepository.ListByBlogAsync(id);
        }

        public async Task<Post> GetAsync(string id)
        {
            var Post = await EnforcePostExistenceAsync(id);

            return Post;
        }

        private async Task<Post> EnforcePostExistenceAsync(string id)
        {
            var post = await _PostRepository.GetAsync(id);

            if (post == null)
            {
                throw new PostNotFoundException(id);
            }

            return post;
        } 
    }
}
