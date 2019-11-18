using System;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class NecessityDTO : Entity, INecessityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Electrical { get; set; }

        public NecessityDTO()
        {
            
        }
    }
}
