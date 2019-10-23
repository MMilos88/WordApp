using Autofac;
using System.Reflection;

namespace DI
{
    internal static class ContainerBuilderExtensions
    {
        internal static void ConfigureAssemblyDI(this ContainerBuilder builder, Assembly assembly, string endsWith)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith(endsWith))
                .AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
