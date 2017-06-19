namespace ProxyMapper.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class TypeUtil
    {
        private static readonly List<Type> PrimtiveTypes = new List<Type>()
        {
            typeof(int),
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(IntPtr),
            typeof(long),
            typeof(Byte),
            typeof(SByte),
            typeof(Boolean),
            typeof(bool),
            typeof(char),
            typeof(string),
            typeof(short),
            typeof(sbyte),
            typeof(DateTime),
            typeof(Double),
            typeof(decimal),
            typeof(double),
            typeof(uint),
            typeof(float),
            typeof(ulong),
            typeof(ushort),
            typeof(UInt16),
            typeof(UInt32),
            typeof(UInt64),
            typeof(UIntPtr)
        };

        public static bool IsPremitive(Type type)
        {
            if (type == null)
            {
                return false;
            }
            Type genericArgument = type;
            if (type.IsGenericParameter && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                genericArgument = type.GetGenericArguments()[0];
            }
            foreach (Type primtiveType in PrimtiveTypes)
            {
                if (primtiveType == genericArgument)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCollection(Type type)
        {
            if (type == null)
            {
                return false;
            }
            return !IsPremitive(type) &&
                   type.GetInterfaces()
                       .Any(implmenetedInterface => implmenetedInterface == typeof(IEnumerable));
        }

        public static bool IsVoid(Type type)
        {
            return type == typeof(void);
        }

        public static IList EmptyList(Type valueType)
        {
            if (valueType == null)
            {
                return null;
            }
            Type listType = typeof(List<>);
            Type constructedListType = listType.MakeGenericType(valueType);
            return (IList) Activator.CreateInstance(constructedListType);
        }
    }
}
