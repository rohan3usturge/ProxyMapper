namespace ProxyMapper.Core
{
    using System.Collections;
    using System.Data.SqlClient;

    public interface IDataProcessor
    {
        IList ProcessDataFromDb(SqlDataReader dataReader, ReturnTypeInfo valueType);
    }
}