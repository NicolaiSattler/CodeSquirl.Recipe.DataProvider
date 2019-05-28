using System;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class MealDTO : Entity, IMealDTO
    {
        public Guid DayPlanID { get; set; }
        public MealType Type { get; set; }
        public Guid RecipyID { get; set; }

        public MealDTO()
        {
            
        }
    }
}
