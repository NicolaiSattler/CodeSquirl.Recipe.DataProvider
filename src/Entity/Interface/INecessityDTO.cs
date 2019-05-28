using System;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface INecessityDTO : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        bool Electrical { get; set; }
    }
}
