using System;
using CodeSquirl.RecipeApp.Model;
using CodeSquirl.System;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class ProductDTO : Entity, IProductDTO
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Perishable { get; set; }

        public ProductDTO()
        {
            
        }
    }
}
