using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IDayPlanDTO : IEntity
    {
        int Weeknumber { get; set; }
        DayName Name { get; set; }
    }
}
