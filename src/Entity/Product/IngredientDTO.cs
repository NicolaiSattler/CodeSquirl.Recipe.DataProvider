using System;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class IngredientDTO : Entity, IIngredientDTO
    {
        public Guid ProductID { get; set; }
        public Guid UnitID { get; set; }

        public IngredientDTO()
        {
            
        }
    }
}
