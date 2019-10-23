using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WordApp.Repository;

namespace DI
{
    public static class AutofacConfigurator
    {
        public static IServiceProvider ConfigureAutofacDI(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(TextDbContext)).As(typeof(DbContext)).InstancePerDependency();

            builder.ConfigureAssemblyDI(GetAssemblyByName("WordApp.BusinessLogic"), "BusinessLogic");
            builder.ConfigureAssemblyDI(GetAssemblyByName("WordApp.Repository"), "Repository");
            builder.ConfigureAssemblyDI(GetAssemblyByName("WordApp.Service"), "Service");

            builder.Populate(services);
            var appContainer = builder.Build();
            return new AutofacServiceProvider(appContainer);
        }

        private static Assembly GetAssemblyByName(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }
    }
}
