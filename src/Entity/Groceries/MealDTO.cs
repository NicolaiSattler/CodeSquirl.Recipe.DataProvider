using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class MealDTO : Entity, IMealDTO
    {
        public MealType Type { get; set; }
        public Guid RecipyID { get; set; }

        public MealDTO()
        {
            
        }
    }
}
