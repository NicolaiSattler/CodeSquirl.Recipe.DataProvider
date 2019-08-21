using System;
using System.Collections.Generic;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class InstructionDTO : Entity, IInstructionDTO
    {
        public Guid ReferenceID { get; }
        public string Description { get; set; }

        public InstructionDTO()
        {
        }
    }
}
