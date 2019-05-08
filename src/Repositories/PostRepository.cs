using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.netcore_22.aws.Models;
using Microsoft.EntityFrameworkCore;

namespace hello.netcore_22.aws.Repositories
{
    public class PostRepository : IPostRepository
    {
         //private readonly IPostMappingService _PostMappingService;
        //private readonly ITableStorageRepository<PostEntity> _PostEntityTableStorageRepository;

        private readonly NorthwindContext _context;
        
        public PostRepository(NorthwindContext context) //IPostMappingService PostMappingService, ITableStorageRepository<PostEntity> PostEntityTableStorageRepository)
        {
             _context = context ?? throw new ArgumentNullException(nameof(context));
            //_PostMappingService = PostMappingService ?? throw new ArgumentNullException(nameof(PostMappingService));
            //_PostEntityTableStorageRepository = PostEntityTableStorageRepository ?? throw new ArgumentNullException(nameof(PostEntityTableStorageRepository));
        }

        public async Task<Post> CreateAsync(Post post)
        {
            //var entityToCreate = _PostMappingService.Map(Post);
            //var createdEntity = await _PostEntityTableStorageRepository.InsertOrReplaceAsync(entityToCreate);
            //var createPost = _PostMappingService.Map(createdEntity);

            var createPost = await _context.Posts.AddAsync(post);
            _context.SaveChanges();
            return createPost.Entity;
        }

        public async Task<Post> DeleteAsync(string id)
        {
            //var deletedEntity = await _PostEntityTableStorageRepository.DeleteOneAsync(clanName, PostKey);
            var deletedPost = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(deletedPost);
            _context.SaveChanges();
            return deletedPost;
        }

        public async Task<IEnumerable<Post>> ListAsync()
        {
            //var entities = await _PostEntityTableStorageRepository.ReadAllAsync();
            //var Post = _PostMappingService.Map(entities);
            //return Post;

            var entities = await _context.Posts.ToListAsync();
            //var Post = _context.Posts.ToList();
            return entities;
        }

        public async Task<Post> UpdateAsync(Post Post)
        {
            //var entityToUpdate = _PostMappingService.Map(Post);
            //var updatedEntity = await _PostEntityTableStorageRepository.InsertOrMergeAsync(entityToUpdate);
              //var deletedEntity = await _PostEntityTableStorageRepository.DeleteOneAsync(clanName, PostKey);
            
            var updatePost = await _context.Posts.FindAsync(Post.Id);
            _context.Posts.Update(Post);
            _context.SaveChanges();
            return updatePost;
        }

        
        public async Task<IEnumerable<Post>> ListByBlogAsync(string blogid)
        {
            var entities = _context.Posts.Where(c => c.BlogId == blogid);
            //var Post = _PostMappingService.Map(entities);
            return await Task.FromResult<IEnumerable<Post>>(entities);
            
        } 

        public Task<Post> GetAsync(string id)
        {
            var post = _context.Posts.FindAsync(id);
            //var Post = _PostMappingService.Map(entity);
            return post;
        }
        

    }
}