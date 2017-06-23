using System;
using System.IO;
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.Extensions.Caching.Distributed;
using ProtoBuf;
using ProxyMapper.Attributes;

namespace ProxyMapper.Core
{
    public class CacheInterceptor : IInterceptor
    {
        private IDistributedCache _distributedCache;

        public CacheInterceptor(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        public void Intercept(IInvocation invocation)
        {
            if (this._distributedCache == null)
            {
                invocation.Proceed();
                return;
            }

            MethodInfo methodInfo = invocation.Method;
            //Attributes-Info
            Cacheable cacheable = methodInfo.GetCustomAttribute<Cacheable>();
            CacheEvict cacheEvict = methodInfo.GetCustomAttribute<CacheEvict>();

            if (cacheable != null && cacheEvict != null)
            {
                throw new ArgumentException("Either CacheAble or CacheEvict attribute can be applied to a method.");
            }

            string cacheableKey = cacheable.Key;
            int cacheableExpiryInMinutes = cacheable.ExpiryInMinutes;
            if (string.IsNullOrWhiteSpace(cacheableKey))
            {
                IKeyGenerator generator = new DefaultKeyGenerator();
                cacheableKey = generator.GenerateKey(methodInfo.DeclaringType.FullName, methodInfo.Name,
                    invocation.Arguments);
            }

            if (cacheable != null)
            {
                byte[] bytes = this._distributedCache.Get(cacheableKey);
                if (bytes != null)
                {
                    object deserialize = Serializer.Deserialize(methodInfo.ReturnType, new MemoryStream(bytes));
                    invocation.ReturnValue = deserialize;
                    return;
                }
                else
                {
                    invocation.Proceed();
                    object invocationReturnValue = invocation.ReturnValue;
                    MemoryStream memoryStream = new MemoryStream();
                    Serializer.Serialize(memoryStream, invocationReturnValue);
                    this._distributedCache.Set(cacheableKey, memoryStream.ToArray());
                    invocation.ReturnValue = invocationReturnValue;
                    return;
                }
            }

            if (cacheEvict != null)
            {
                this._distributedCache.Remove(cacheableKey);
            }
        }
    }
}
