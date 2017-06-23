using System;

namespace ProxyMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Cacheable : Attribute
    {
        public string Key { get; set; }
        public int ExpiryInMinutes { get; set; }

        public Cacheable(string key, int expiryInMinutes)
        {
            this.Key = key;
            this.ExpiryInMinutes = expiryInMinutes;
        }

        public Cacheable()
        {
            
        }
    }
}