using System;
using System.Collections.Generic;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IInstructionDTO : IEntity
    {
        Guid ReferenceID { get; }
        string Description { get; set; }
    }
}
