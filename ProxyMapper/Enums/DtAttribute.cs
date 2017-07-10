using System;

namespace ProxyMapper.Enums
{
    public class DtAttribute : Attribute
    {
        public DtAttribute(string columns)
        {
            this.Columns = columns;
        }

        public string Columns { get; }
    }
}
