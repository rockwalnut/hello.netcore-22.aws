using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.netcore_22.aws.Models;
using Microsoft.EntityFrameworkCore;

namespace hello.netcore_22.aws.Repositories
{
    public class BlogRepository : IBlogRepository
    {
         //private readonly IBlogMappingService _BlogMappingService;
        //private readonly ITableStorageRepository<BlogEntity> _BlogEntityTableStorageRepository;

        private readonly NorthwindContext _context;
        
        public BlogRepository(NorthwindContext context) //IBlogMappingService BlogMappingService, ITableStorageRepository<BlogEntity> BlogEntityTableStorageRepository)
        {
             _context = context ?? throw new ArgumentNullException(nameof(context));
            //_BlogMappingService = BlogMappingService ?? throw new ArgumentNullException(nameof(BlogMappingService));
            //_BlogEntityTableStorageRepository = BlogEntityTableStorageRepository ?? throw new ArgumentNullException(nameof(BlogEntityTableStorageRepository));
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            //var entityToCreate = _BlogMappingService.Map(Blog);
            //var createdEntity = await _BlogEntityTableStorageRepository.InsertOrReplaceAsync(entityToCreate);
            //var createBlog = _BlogMappingService.Map(createdEntity);

            var createBlog = await _context.Blogs.AddAsync(blog);
            _context.SaveChanges();
            return createBlog.Entity;
        }

        public async Task<Blog> DeleteAsync(string id)
        {
            //var deletedEntity = await _BlogEntityTableStorageRepository.DeleteOneAsync(clanName, BlogKey);
            var deletedBlog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(deletedBlog);
            _context.SaveChanges();
            return deletedBlog;
        }

        public async Task<IEnumerable<Blog>> ListAsync()
        {
            //var entities = await _BlogEntityTableStorageRepository.ReadAllAsync();
            //var Blog = _BlogMappingService.Map(entities);
            //return Blog;

            var entities = await _context.Blogs.ToListAsync();
            //var Blog = _context.Blogs.ToList();
            return entities;
        }

        public async Task<Blog> UpdateAsync(Blog Blog)
        {
            //var entityToUpdate = _BlogMappingService.Map(Blog);
            //var updatedEntity = await _BlogEntityTableStorageRepository.InsertOrMergeAsync(entityToUpdate);
              //var deletedEntity = await _BlogEntityTableStorageRepository.DeleteOneAsync(clanName, BlogKey);
            
            var updateBlog = await _context.Blogs.FindAsync(Blog.Id);
            _context.Blogs.Update(Blog);
            _context.SaveChanges();
            return updateBlog;
        }

        /* 
        public async Task<IEnumerable<Blog>> ReadAllInClanAsync(string clanName)
        {
            var entities = await _BlogEntityTableStorageRepository.ReadPartitionAsync(clanName);
            var Blog = _BlogMappingService.Map(entities);
            return Blog;
        } */

        public Task<Blog> GetAsync(string BlogKey)
        {
            var blog = _context.Blogs.FindAsync(BlogKey);
            //var Blog = _BlogMappingService.Map(entity);
            return blog;
        }
        

    }
}