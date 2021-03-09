using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using IRepository;
using CookBook.Controllers;
using CookBook.Models;
using CookBookModel;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class RecipeTest
    {

        [Fact]
        public async Task GetRecipe()
        {
            var recipies = new Recipe[]
            {
                new Recipe
                {
                    Id = Guid.Parse("01234567-9012-3456-7890-123456789012"),
                    Level = HierarchyId.Parse("/1/1/"),
                    Title = "Chicken",
                    Description = "Take chicken",
                    CreateTime = new DateTime(2021, 03, 05, 23, 00, 15),
                },

                new Recipe
                {
                    Id = Guid.Parse("00234557-9000-3456-7890-123456788044"),
                    Level = HierarchyId.Parse("/1/2/"),
                    Title = "Beef",
                    Description = "Take beef",
                    CreateTime = new DateTime(2021, 03, 06, 01, 15, 22),
                },
            };

            var moq = new Mock<ICookBookRepository>();
            moq.Setup(i => i.Get(recipies[0].Id.ToString())).Returns(Task.FromResult(recipies[0]));
            var controller = new RecipeController(moq.Object);

            var result = await controller.Get(recipies[0].Id.ToString());

            Assert.Equal("Chicken", result.Value.Title);
        }
    }
}
