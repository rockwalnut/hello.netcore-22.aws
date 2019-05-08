
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.netcore_22.aws.Models;

namespace hello.netcore_22.aws.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> ListAsync();
        //Task<IEnumerable<Post>> ReadAllInClanAsync(string clanName);
        
        Task<Post> GetAsync(string id);
        Task<Post> CreateAsync(Post post);
        Task<Post> UpdateAsync(Post post);
        Task<Post> DeleteAsync(string id);

        Task<IEnumerable<Post>> ListByBlogAsync(string id);
    }
}
