using System;

namespace ProxyMapper.Enums
{
    public class DatatableAttribute : Attribute
    {
        public DatatableAttribute(string columns)
        {
            this.Columns = columns;
        }

        public string Columns { get; }
    }
}
