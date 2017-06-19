namespace ProxyMapper.Core
{
    using System;
    using System.Reflection;
    using ProxyMapper.Util;

    public class ReturnTypeInfo
    {
        private readonly Type _returnType;

        public ReturnTypeInfo(Type returnType)
        {
            this._returnType = returnType;
            this.IsVoid = TypeUtil.IsVoid(returnType);
            this.IsCollection = TypeUtil.IsCollection(returnType);
            this.IsPrimitive = TypeUtil.IsPremitive(this.ActualType);
        }

        public bool IsCollection { get; }

        public bool IsPrimitive { get; }

        public bool IsVoid { get; private set; }

        public Type ActualType => this.IsCollection ? this._returnType.GetGenericArguments()[0] : this._returnType;
    }
}