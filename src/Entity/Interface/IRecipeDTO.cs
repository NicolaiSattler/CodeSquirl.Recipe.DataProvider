using System;
using System.Collections.Generic;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IRecipeDTO : IEntity
    {
        Guid UserID { get; set; }
        string Name { get; set; }
        RecipeType Type { get; set; }
        DietType Diet { get; set; }
        TimeSpan Duration { get; }
        bool AllowRemnants { get; set; }
    }
}
