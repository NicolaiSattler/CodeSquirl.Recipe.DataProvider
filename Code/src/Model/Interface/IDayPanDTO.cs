using System;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IDayPlanDTO : IEntity
    {
        Guid GroceryListID { get; set; }
        DayName Name { get; set; }
    }
}
