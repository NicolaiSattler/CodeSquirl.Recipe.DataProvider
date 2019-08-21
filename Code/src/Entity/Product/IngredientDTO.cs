using System;
using System.Runtime.Serialization;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    [Serializable]
    public class IngredientDTO : Entity, IIngredientDTO
    {
        public Guid ProductID { get; set; }
        public Guid UnitID { get; set; }

        protected IngredientDTO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ProductID = (Guid)info.GetValue(nameof(ProductID), typeof(Guid));
            UnitID = (Guid)info.GetValue(nameof(UnitID), typeof(Guid));
        }
        public IngredientDTO()
        {
            
        }
    }
}
