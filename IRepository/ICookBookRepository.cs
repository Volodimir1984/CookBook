using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CookBookModel;

namespace IRepository
{
    public interface ICookBookRepository
    {
        (IEnumerable<Tuple<string, string>>, IEnumerable<Recipe>) Data { get; set; }
        Task Create(string level, string title, string description);
        Task Update(string id, string title, string description);
        Task<(IEnumerable<Tuple<string, string>>, IEnumerable<Recipe>)> GetLevel(string level);
        Task<Recipe> Get(string id);
        Task<Recipe> SearchWithTitle(string title);
    }
}