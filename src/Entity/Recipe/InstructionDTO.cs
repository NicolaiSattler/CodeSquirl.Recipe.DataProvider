using System;
using System.Collections.Generic;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
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
