using System;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class IngredientDTO : Entity, IIngredientDTO
    {
        public Guid ProductID { get; set; }
        public Guid AmountID { get; set; }

        public IngredientDTO()
        {
            
        }
    }
}
