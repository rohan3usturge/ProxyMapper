using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace ProxyMapper.Core
{
    public static class ProxyFactory
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static T CreateRepoProxy<T>(string connectionString, IDataProcessor dataProcessor = null) where T : class
        {
            if (!typeof(T).GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException(
                    "Proxies can be generated only for interfaces. Please provide a proper interface.");
            }
            object proxy = ProxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T),
                new DbCallInterceptor(connectionString, GetDataProcessor(dataProcessor),
                    new DefaultDatatableConverter()));
            return (T) proxy;
        }

        private static IDataProcessor GetDataProcessor(IDataProcessor dataProcessor)
        {
            IDataProcessor dataProcessorObj = dataProcessor ?? new DefaultDataProcessor();
            return dataProcessorObj;
        }
    }
}