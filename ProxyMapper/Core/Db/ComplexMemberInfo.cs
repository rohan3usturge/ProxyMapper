namespace ProxyMapper.Core
{
    public class ComplexMemberInfo
    {
        public ComplexMemberInfo(string name, bool ignore = false)
        {
            this.Name = name;
            this.Ignore = ignore;
        }

        public string Name { get; private set; }

        public bool Ignore { get; private set; }
    }
}