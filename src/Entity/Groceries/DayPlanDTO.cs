using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class DayPlanDTO : Entity, IDayPlanDTO
    {
        public int Weeknumber { get; set; }
        public DayName Name { get; set; }

        public DayPlanDTO()
        {
            
        }
    }
}
