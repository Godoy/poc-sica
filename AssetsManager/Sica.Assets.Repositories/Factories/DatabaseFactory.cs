using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace Sica.Assets.Repositories.Factories
{
    internal static class DatabaseFactory
    {
        public static DbProviderFactory GetDbProviderFactory(string dbFactoryTypeName, string assemblyName)
        {
            Type factoryType = GetDbFactoryType(dbFactoryTypeName, assemblyName);

            var instance = GetDbFactoryInstance(factoryType, assemblyName);

            return instance;
        }

        private static Type GetDbFactoryType(string factoryTypeName, string assemblyName)
        {

            var assembly = Assembly.Load(assemblyName);
            if (assembly is null)
                throw new InvalidOperationException($"Can't load assembly {assemblyName}");

            var factoryType = assembly.GetType(factoryTypeName, false);
            if (factoryType is null)
                throw new InvalidOperationException($"Can't get db factory type {factoryTypeName} from assembly {assemblyName}");

            if (!typeof(DbProviderFactory).IsAssignableFrom(factoryType))
                throw new InvalidOperationException($"Db factory type {factoryTypeName} isn't assignable from {typeof(DbProviderFactory)}");

            return factoryType;
        }

        private static DbProviderFactory GetDbFactoryInstance(Type factoryType, string assemblyName)
        {
            var instance = GetStaticProperty(factoryType, "Instance");

            if (instance == null)
            {
                var assemblyLoaded = Assembly.Load(assemblyName);
                if (assemblyLoaded != null)
                    instance = GetStaticProperty(factoryType, "Instance");
            }

            if (instance == null)
                throw new InvalidOperationException($"Can't get instance from db factory type {factoryType?.FullName}");

            return instance as DbProviderFactory;
        }

        private static object GetStaticProperty(Type type, string property)
        {
            try
            {
                return type.InvokeMember(property,
                                            BindingFlags.Static |
                                            BindingFlags.Public |
                                            BindingFlags.GetField |
                                            BindingFlags.GetProperty,
                                            null, type, null);
            }
            catch
            {
                return null;
            }
        }
    }
}
