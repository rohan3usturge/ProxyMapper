using Castle.DynamicProxy;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace ProxyMapper.Core
{
    public class ProxyFactory
    {
        private readonly ILogger _logger;
        private readonly IDistributedCache _cacheManager;

        public ProxyFactory(ILogger logger, IDistributedCache cacheManager)
        {
            this._logger = logger;
            this._cacheManager = cacheManager;
        }

        public T CreateProxy<T>(string connectionString, IDataProcessor dataProcessor = null) where T : class
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            object proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T), new CacheInterceptor(_cacheManager),
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