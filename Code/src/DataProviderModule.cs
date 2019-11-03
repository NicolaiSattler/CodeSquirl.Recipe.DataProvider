using Autofac;
using CodeSquirrel.System;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class DataProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DayPlanDTO>().As<IDayPlanDTO>();
            builder.RegisterType<GroceryListDTO>().As<IGroceryListDTO>();
            builder.RegisterType<MealDTO>().As<IMealDTO>();
            builder.RegisterType<IngredientDTO>();
            builder.RegisterType<NecessityDTO>();
            builder.RegisterType<ProductDTO>();
            builder.RegisterType<UnitDTO>();
            builder.RegisterType<InstructionDTO>();
            builder.RegisterType<PreparationDTO>();
            builder.RegisterType<RecipeDTO>();

            builder.RegisterType<ProductRepository>().As<IRepository<ProductDTO>>();
            builder.RegisterType<NecessityRepository>().As<IRepository<NecessityDTO>>();
            builder.RegisterType<RecipeRepository>().As<IRepository<RecipeDTO>>();
            builder.RegisterType<UnitRepository>().As<IRepository<UnitDTO>>();
        }
    }
}