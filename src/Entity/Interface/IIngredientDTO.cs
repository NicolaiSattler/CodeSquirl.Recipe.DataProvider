using System;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IIngredientDTO : IEntity
    {
        Guid ProductID { get; set; }
        Guid AmountID { get; set; }
    }
}
