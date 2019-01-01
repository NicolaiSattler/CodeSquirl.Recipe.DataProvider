using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IMealDTO : IEntity
    {
        MealType Type { get; set; }
        Guid RecipyID { get; set; }
    }
}
