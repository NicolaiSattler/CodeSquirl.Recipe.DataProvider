using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IProductDTO : IEntity
    {
        string Name { get; set; }
        int Type { get; set; }
        bool Perishable { get; set; }
    }
}
