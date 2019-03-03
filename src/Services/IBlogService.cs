
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
        //Task<Ninja> ReadOneAsync(string clanName, string ninjaKey);
        Task<Blog> CreateAsync(Blog ninja);
        Task<Blog> UpdateAsync(Blog ninja);
        Task<Blog> DeleteAsync(string id);
    }
}
