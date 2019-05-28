using System;
using System.Collections.Generic;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IInstructionDTO : IEntity
    {
        Guid ReferenceID { get; }
        string Description { get; set; }
    }
}
