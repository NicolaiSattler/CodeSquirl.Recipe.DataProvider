using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IUnitDTO : IEntity
    {
        int Value { get; set; }
        UnitType Type { get; set; }
    }
}
