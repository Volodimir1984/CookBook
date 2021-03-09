using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using IRepository;

namespace CookBook.Controllers
{
    [ApiController]
    [Route("api/recipe")]
    public class RecipeController: ControllerBase
    {
        private readonly ICookBookRepository _repository;

        public RecipeController(ICookBookRepository repo)
        {
            _repository = repo;
        }

        [HttpPost("create")]
        public async Task<ActionResult<RecipeApi>> Post(RecipeApi recipe)
        {
            
            if (await _repository.SearchWithTitle(recipe.Title) != null)
                return new RecipeApi();

            var level = recipe.Level.Replace('-', '/');

            await _repository.Create($"/{level}/", recipe.Title, recipe.Description);
            var result = await _repository.SearchWithTitle(recipe.Title);

            return new RecipeApi
            {
                Id = PrepareLevel(result.Id.ToString()),
                Title = result.Title,
            };
        }

        [HttpPost("update")]
        public async Task<ActionResult<RecipeApi>> Post(RecipeUpdateApi recipe)
        {
            await _repository.Update(recipe.Id, recipe.Title, recipe.Description);
            
            var upadateRecipe = await _repository.Get(recipe.Id);
            
            return new RecipeApi
            {
                Id = upadateRecipe.Id.ToString(),
                Level = PrepareLevel(upadateRecipe.Level.ToString()),
                Title = upadateRecipe.Title,
                Description = upadateRecipe.Description,
                CreateTime = upadateRecipe.CreateTime,
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeApi>> Get(string id)
        {
            var result = await _repository.Get(id);

            return new RecipeApi
            {
                Id = result.Id.ToString(),
                Level = PrepareLevel(result.Level.ToString()),
                Title = result.Title,
                Description = result.Description,
                CreateTime = result.CreateTime,
            };
        }

        [HttpGet("list/{level}")]
        public async Task<ListRecipeApi> Get(string level, bool? isSort)
        {
            _repository.Data = await _repository.GetLevel($"/{level.Replace('-', '/')}/");

            if (isSort == true)
            {

                return new ListRecipeApi
                {
                    Parents = _repository.Data.Item1.Select(i => new ParentApi
                    {
                        Level = PrepareLevel(i.Item1).Length == 1 ? "0" : PrepareLevel(i.Item1),
                        Title = i.Item2,
                    }).ToList(),
                    Recipes = _repository.Data.Item2.OrderBy(i => i.Title).Select(i => new RecipeApi
                    {
                        Id = i.Id.ToString(),
                        Level = PrepareLevel(i.Level.ToString()),
                        Title = i.Title,
                        CreateTime = i.CreateTime,
                    }).ToList(),
                };
            }
            if (isSort == false)
            {
                return new ListRecipeApi
                {
                    Parents = _repository.Data.Item1.Select(i => new ParentApi
                    {
                        Level = PrepareLevel(i.Item1).Length == 1 ? "0" : PrepareLevel(i.Item1),
                        Title = i.Item2,
                    }).ToList(),
                    Recipes = _repository.Data.Item2.OrderByDescending(i => i.Title).Select(i => new RecipeApi
                    {
                        Id = i.Id.ToString(),
                        Level = PrepareLevel(i.Level.ToString()),
                        Title = i.Title,
                        CreateTime = i.CreateTime,
                    }).ToList(),
                };
            }
            
            return new ListRecipeApi
            {
                Parents = _repository.Data.Item1.Select(i => new ParentApi
                {
                    Level = PrepareLevel(i.Item1).Length == 1 ? "0" : PrepareLevel(i.Item1),
                    Title = i.Item2,
                }).ToList(),
                Recipes = _repository.Data.Item2.Select(i => new RecipeApi
                {
                    Id = i.Id.ToString(),
                    Level = PrepareLevel(i.Level.ToString()),
                    Title = i.Title,
                    CreateTime = i.CreateTime,
                }).ToList(),
            };
        }
             

        private string PrepareLevel(string level)
        {
            return level.Replace("/", "-").Trim(new []{'-'});
        }
    }
}
