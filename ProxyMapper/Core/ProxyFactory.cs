namespace ProxyMapper.Core
{
    using Castle.DynamicProxy;

    public class ProxyFactory
    {
        private static T CreateProxy<T>(string connectionString, IDataProcessor dataProcessor) where T : class
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            object proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T),
                new DbCallInterceptor(connectionString, GetDataProcessor(dataProcessor)));
            return (T) proxy;
        }

        private static IDataProcessor GetDataProcessor(IDataProcessor dataProcessor)
        {
            IDataProcessor dataProcessorObj = dataProcessor ?? new DefaultDataProcessor();
            return dataProcessorObj;
        }
    }
}