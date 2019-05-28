using System;
using System.Collections.Generic;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
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
