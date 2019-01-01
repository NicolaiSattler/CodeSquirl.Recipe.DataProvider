using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class UnitDTO : Entity, IUnitDTO
    {
        public int Value { get; set; }
        public UnitType Type { get; set; }

        public UnitDTO()
        {

        }
    }
}
