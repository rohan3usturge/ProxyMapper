using System.Collections;
using System.Data;

namespace ProxyMapper.Core
{
    public interface IDatatableConverter
    {
        DataTable ToDataTable(IEnumerable list);
    }
}
