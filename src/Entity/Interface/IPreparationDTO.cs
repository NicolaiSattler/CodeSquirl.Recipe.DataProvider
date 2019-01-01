using System;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public interface IPreparationDTO : IEntity
    {
        Guid RecipyId { get; }
        Guid InstructionID { get; }
        TimeSpan Duration { get; }
        TimeSpan WaitTime { get; }
    }
}
