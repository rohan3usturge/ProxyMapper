using ProxyMapper.Util;

namespace ProxyMapper.Core
{
    public class DefaultKeyGenerator : IKeyGenerator
    {
        public string GenerateKey(string target, string method, params object[] arguments)
        {
            target.AssertNotNullOrEmpty("Target cannot be empty");
            method.AssertNotNullOrEmpty("method cannot be empty");
            arguments.AssertNotNull("arguments cannot be null");
            return new DefaultKey(arguments, method, target).GetHashCode().ToString();
        }
    }
}
