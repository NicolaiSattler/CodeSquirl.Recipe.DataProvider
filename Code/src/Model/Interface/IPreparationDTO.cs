using System;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public interface IPreparationDTO : IEntity
    {
        Guid RecipyId { get; }
        Guid InstructionID { get; }
        TimeSpan Duration { get; }
        TimeSpan WaitTime { get; }
    }
}
