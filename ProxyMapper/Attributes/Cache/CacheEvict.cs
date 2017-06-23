using System;

namespace ProxyMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CacheEvict : Attribute
    {
        public string Key { get; set; }

        public CacheEvict(string key)
        {
            this.Key = key;
        }

        public CacheEvict()
        {
        }
    }
}