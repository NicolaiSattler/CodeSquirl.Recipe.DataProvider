using System;
using System.Collections.Generic;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IGroceryListDTO : IEntity
    {
        int WeekNummer { get; set; }
        Guid UserID { get; set; }
    }
}
