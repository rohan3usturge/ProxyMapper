namespace ProxyMapper.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using ProxyMapper.Util;

    public class DefaultDataProcessor : IDataProcessor
    {
        //protected ILogger Logger { get; set; }

        public virtual IList ProcessDataFromDb(SqlDataReader sqlDataReader, ReturnTypeInfo valueType)
        {
            return ProcessOneResultSet(sqlDataReader, valueType);
        }

        protected static IList ProcessOneResultSet(IDataReader sqlDataReader, ReturnTypeInfo valueType)
        {
            IList list = TypeUtil.EmptyList(valueType.ActualType);
            bool isFirstRow = true;
            Dictionary<int, PropertyInfo> ordinalPropertyMap = null;
            while (sqlDataReader.Read())
            {
                object mainObj = null;
                if (valueType.IsPrimitive)
                {
                    if (!sqlDataReader.IsDBNull(0))
                    {
                        mainObj = sqlDataReader.GetValue(0);
                    }
                }
                else
                {
                    if (isFirstRow)
                    {
                        ordinalPropertyMap = PopulateOrdinalPropertyMap(sqlDataReader,valueType.ActualType);
                        isFirstRow = false;
                    }
                    mainObj = GetOneRow(sqlDataReader, valueType, ordinalPropertyMap);
                }
                if (mainObj != null)
                {
                    list.Add(mainObj);
                }
            }
            return list;
        }

        private static Dictionary<int, PropertyInfo> PopulateOrdinalPropertyMap(IDataRecord sqlDataReader,Type valueType)
        {
            Dictionary<int, PropertyInfo> ordinalPropetyMap = new Dictionary<int, PropertyInfo>();
            for (int i = 0; i < sqlDataReader.FieldCount; i++)
            {
                PropertyInfo propertyInfo = valueType.GetProperty(sqlDataReader.GetName(i),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    ordinalPropetyMap.Add(i, propertyInfo);
                }
            }
            return ordinalPropetyMap;
        }

        private static object GetOneRow(IDataRecord sqlDataReader, ReturnTypeInfo valueType,
            IReadOnlyDictionary<int, PropertyInfo> ordinalPropertyMap)
        {
            object oneRowInstance = Activator.CreateInstance(valueType.ActualType);
            for (int j = 0; j < sqlDataReader.FieldCount; j++)
            {
                if (sqlDataReader.IsDBNull(j) || !ordinalPropertyMap.ContainsKey(j))
                {
                    continue;
                }
                PropertyInfo populateOrdinalProperty = ordinalPropertyMap[j];
                populateOrdinalProperty.SetValue(oneRowInstance, sqlDataReader.GetValue(j));
            }
            return oneRowInstance;
        }
    }
}