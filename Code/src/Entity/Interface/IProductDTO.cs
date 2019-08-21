using System;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IProductDTO : IEntity
    {
        string Name { get; set; }
        int Type { get; set; }
        bool Perishable { get; set; }
    }
}
