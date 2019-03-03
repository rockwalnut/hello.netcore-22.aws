
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//model
using hello.netcore_22.aws.Models;
using hello.netcore_22.aws.Repositories;

namespace hello.netcore_22.aws.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _BlogRepository;
        //private readonly IClanService _clanService;

        public BlogService(IBlogRepository BlogRepository) //, IClanService clanService)
        {
            _BlogRepository = BlogRepository ?? throw new ArgumentNullException(nameof(BlogRepository));
            //_clanService = clanService ?? throw new ArgumentNullException(nameof(clanService));
        }

        public async Task<Blog> CreateAsync(Blog Blog)
        {
            //await EnforceClanExistenceAsync(Blog.Clan.Name);
            var createdBlog = await _BlogRepository.CreateAsync(Blog);
            return createdBlog;
        }


        public async Task<Blog> UpdateAsync(Blog Blog)
        {
           // await EnforceClanExistenceAsync(Blog.Clan.Name);
            //await EnforceBlogExistenceAsync(Blog.Clan.Name, Blog.Key);
            var updatedBlog = await _BlogRepository.UpdateAsync(Blog);
            return updatedBlog;
        }

        public async Task<Blog> DeleteAsync(string id)
        {
           // await EnforceBlogExistenceAsync(clanName, BlogKey);
            var deletedBlog = await _BlogRepository.DeleteAsync(id);
            return deletedBlog;
        }

        public Task<IEnumerable<Blog>> ListAsync()
        {
            return _BlogRepository.ListAsync();
        }

       /*  public async Task<IEnumerable<Blog>> ReadAllInClanAsync(string clanName)
        {
            await EnforceClanExistenceAsync(clanName);
            return await _BlogRepository.ReadAllInClanAsync(clanName);
        }

        public async Task<Blog> ReadOneAsync(string clanName, string BlogKey)
        {
            var Blog = await EnforceBlogExistenceAsync(clanName, BlogKey);
            return Blog;
        }


        private async Task EnforceClanExistenceAsync(string clanName)
        {
            var clanExist = await _clanService.IsClanExistsAsync(clanName);
            if (!clanExist)
            {
                throw new ClanNotFoundException(clanName);
            }
        }

        private async Task<Blog> EnforceBlogExistenceAsync(string clanName, string BlogKey)
        {
            var remoteBlog = await _BlogRepository.ReadOneAsync(clanName, BlogKey);
            if (remoteBlog == null)
            {
                throw new BlogNotFoundException(clanName, BlogKey);
            }
            return remoteBlog;
        } */
    }
}