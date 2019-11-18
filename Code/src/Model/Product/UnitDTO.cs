using System;
using System.Runtime.Serialization;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    [Serializable]
    public class UnitDTO : Entity, IUnitDTO
    {
        public int Value { get; set; }
        public int Type { get; set; }

        protected UnitDTO(SerializationInfo info, StreamingContext context)
        {
            Value = info.GetInt32(nameof(Value));
            Type = info.GetInt32(nameof(Type));
        }
        public UnitDTO()
        {

        }
    }
}
