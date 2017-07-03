using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace ProxyMapper.Core
{
    public class ProxyFactory
    {
        private readonly ILogger _logger;

        public ProxyFactory(ILogger logger)
        {
            this._logger = logger;
        }

        public T CreateProxy<T>(string connectionString, IDataProcessor dataProcessor = null) where T : class
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            object proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T), new DbCallInterceptor(connectionString, GetDataProcessor(dataProcessor)));
            return (T) proxy;
        }

        private static IDataProcessor GetDataProcessor(IDataProcessor dataProcessor)
        {
            IDataProcessor dataProcessorObj = dataProcessor ?? new DefaultDataProcessor();
            return dataProcessorObj;
        }
    }
}