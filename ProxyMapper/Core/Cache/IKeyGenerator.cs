namespace ProxyMapper.Core
{
    public interface IKeyGenerator
    {
        string GenerateKey(string target, string method, params object[] arguments);
    }
}
