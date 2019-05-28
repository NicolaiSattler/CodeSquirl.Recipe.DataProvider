using System;
using System.Collections.Generic;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IGroceryListDTO : IEntity
    {
        int WeekNummer { get; set; }
        Guid UserID { get; set; }
    }
}
