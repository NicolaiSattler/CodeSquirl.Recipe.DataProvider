using System;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class DayPlanDTO : Entity, IDayPlanDTO
    {
        public Guid GroceryListID { get; set; }
        public DayName Name { get; set; }

        public DayPlanDTO()
        {
            
        }
    }
}
