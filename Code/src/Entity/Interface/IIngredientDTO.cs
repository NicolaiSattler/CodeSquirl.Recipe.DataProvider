using System;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IIngredientDTO : IEntity
    {
        Guid ProductID { get; set; }
        Guid UnitID { get; set; }
    }
}
