using System.Collections;
using System.Data.SqlClient;

namespace ProxyMapper.Core
{
    public interface IDataProcessor
    {
        IList ProcessDataFromDb(SqlDataReader dataReader, ReturnTypeInfo valueType);
    }
}