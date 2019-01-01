using System;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class PreparationDTO : Entity, IPreparationDTO
    {
        public Guid RecipyId { get; }
        public Guid InstructionID { get; }
        public TimeSpan Duration { get; }
        public TimeSpan WaitTime { get; }

        public PreparationDTO()
        {
        }
    }
}
