using System;
using System.Collections.Generic;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class RecipeDTO : Entity, IRecipeDTO
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public RecipeType Type { get; set; }
        public DietType Diet { get; set; }
        public TimeSpan Duration { get; }
        public bool AllowRemnants { get; set; }

        public RecipeDTO()
        {
            
        }
    }
}
