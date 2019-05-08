using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.netcore_22.aws.Models;

namespace hello.netcore_22.aws.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> ListAsync();
       // Task<IEnumerable<Blog>> ReadAllInClanAsync(string clanName);
        Task<Blog> GetAsync(string BlogKey);
        Task<Blog> CreateAsync(Blog Blog);
        Task<Blog> UpdateAsync(Blog Blog);
        Task<Blog> DeleteAsync(string id);
    }
}