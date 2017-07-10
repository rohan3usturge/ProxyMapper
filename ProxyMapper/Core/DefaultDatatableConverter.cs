using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace ProxyMapper.Core
{
    public class DefaultDatatableConverter : IDatatableConverter
    {
        public DataTable ToDataTable(IEnumerable list)
        {
            Type type = list.GetType().GetGenericArguments()[0];
            PropertyInfo[] properties = type.GetProperties();
            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (object entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
