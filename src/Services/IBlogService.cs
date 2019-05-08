
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.netcore_22.aws.Models;

namespace hello.netcore_22.aws.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> ListAsync();
        //Task<IEnumerable<Blog>> ReadAllInClanAsync(string clanName);
        
        Task<Blog> GetAsync(string id);
        Task<Blog> CreateAsync(Blog blog);
        Task<Blog> UpdateAsync(Blog blog);
        Task<Blog> DeleteAsync(string id);
    }
}
