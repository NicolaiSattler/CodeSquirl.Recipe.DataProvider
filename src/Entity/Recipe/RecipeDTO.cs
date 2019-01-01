using System;
using System.Collections.Generic;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class RecipeDTO : Entity, IRecipeDTO
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public RecipyType Type { get; set; }
        public DietType Diet { get; set; }
        public TimeSpan Duration { get; }
        public bool AllowRemnants { get; set; }

        public RecipeDTO()
        {
            
        }
    }
}
