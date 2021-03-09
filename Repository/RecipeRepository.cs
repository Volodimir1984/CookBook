using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IRepository;
using CookBookModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RecipeRepository : ICookBookRepository
    {
        public (IEnumerable<Tuple<string, string>>, IEnumerable<Recipe>) Data { get; set; }

        private CookBookContext _db;

        public RecipeRepository()
        {
            _db = new CookBookContext();
        }

        public async Task Create(string level, string title, string description)
        {
            int resultLevel;
            string result;

            if (level == "/0/")
            {
                resultLevel = await _db.Recipes.Where(i => i.Level.GetLevel() == 1).CountAsync();
                result = $"/{++resultLevel}/";
            }
            else
            {
                resultLevel = await _db.Recipes.Where(i => i.Level.IsDescendantOf(HierarchyId.Parse(level)) 
                               && i.Level.GetLevel() == level.Replace("/", "").Length + 1).CountAsync();
                result = $"{level}{++resultLevel}/";
            }
            
            var recipe = new Recipe
            {
                Level = HierarchyId.Parse(result) ,
                Title = title,
                Description = description,
                CreateTime = DateTime.Now,
            };

            await _db.AddAsync(recipe);

            _db.SaveChanges();
            
        }

        public async Task<Recipe> Get(string id) 
        { 

            var recipe = await _db.Recipes.AsNoTracking().Where(i => i.Id.ToString() == id).FirstOrDefaultAsync();

            return recipe;
        }

        public async Task<Recipe> SearchWithTitle(string title)
        {
            var recipe = await _db.Recipes.AsNoTracking().Where(i => i.Title == title).FirstOrDefaultAsync();

            return recipe;
        }

        public async Task<(IEnumerable<Tuple<string, string>>, IEnumerable<Recipe>)> GetLevel(string level)
        {
            var recipes = new List<Recipe>();
            var parent = new List<Tuple<string, string>>();

            if (level == "/0/")
                recipes = await _db.Recipes.AsTracking().Where(i => i.Level.GetLevel() == 1).ToListAsync();
            else
            {
                recipes = await _db.Recipes.AsTracking().Where(i => i.Level.IsDescendantOf(HierarchyId.Parse(level))
                                                                    && i.Level.GetLevel() ==
                                                                    level.Replace("/", "").Length + 1).ToListAsync();
                
                parent = await _db.Recipes.Where(i => HierarchyId.Parse(level).IsDescendantOf(i.Level))
                    .Select(i => new Tuple<string, string> (i.Level.ToString(), i.Title)).ToListAsync();
            }

            return (parent, recipes);
        }

        public async Task Update(string id, string title, string description)
        {
            var recipe = await Get(id);

            recipe.Title = title;
            recipe.Description = description;

            _db.Recipes.Update(recipe);

            _db.SaveChanges();
        }
    }
}
