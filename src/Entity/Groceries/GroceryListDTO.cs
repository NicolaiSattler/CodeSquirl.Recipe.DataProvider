using System;
using System.Collections.Generic;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class GroceryListDTO : Entity, IGroceryListDTO
    {
        public int WeekNummer { get; set; }
        public Guid UserID { get; set; }

        public GroceryListDTO()
        {
        }
    }
}
