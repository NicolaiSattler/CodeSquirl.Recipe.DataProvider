using System;
using System.Runtime.Serialization;
using CodeSquirrel.RecipeApp.Model;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    [Serializable]
    public class ProductDTO : Entity, IProductDTO
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Perishable { get; set; }

        protected ProductDTO(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(nameof(Name));
            Type = info.GetInt32(nameof(Type));
            Perishable = info.GetBoolean(nameof(Perishable));
        }
        public ProductDTO()
        {
            
        }
        

    }
}
