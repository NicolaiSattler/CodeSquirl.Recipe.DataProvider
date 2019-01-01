using System;
using System.Collections.Generic;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IRecipeDTO : IEntity
    {
        Guid UserID { get; set; }
        string Name { get; set; }
        RecipyType Type { get; set; }
        DietType Diet { get; set; }
        TimeSpan Duration { get; }
        bool AllowRemnants { get; set; }
    }
}
