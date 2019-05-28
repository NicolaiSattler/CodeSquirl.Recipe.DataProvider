using System;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
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
