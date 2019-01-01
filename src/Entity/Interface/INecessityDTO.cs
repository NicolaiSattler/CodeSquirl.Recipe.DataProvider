using System;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface INecessityDTO : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        bool Electrical { get; set; }
    }
}
