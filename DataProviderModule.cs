using Autofac;

namespace CodeSquirl.Recipy.DataProvider
{
    public class DataProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>();
        }
    }
}