using System;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IMealDTO : IEntity
    {
        MealType Type { get; set; }
        Guid RecipyID { get; set; }
    }
}
