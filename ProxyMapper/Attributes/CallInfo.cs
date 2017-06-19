namespace ProxyMapper.Attributes
{
    using System;
    using ProxyMapper.Enums;

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CallInfo : Attribute
    {
        public CallInfo()
        {
        }

        public CallInfo(string queryString)
        {
            this.QueryString = queryString;
            this.CallType = CallType.Procedure;
        }

        public CallInfo(string queryString,bool cacheAble,int expiryInMinutes)
        {
            this.QueryString = queryString;
            this.CallType = CallType.Procedure;
            this.CacheAble = cacheAble;
            this.ExpiryInMinutes = expiryInMinutes;
        }

        public CallInfo(string queryString, CallType callType)
        {
            this.QueryString = queryString;
            this.CallType = callType;
        }

        public string QueryString { get; set; }

        public bool CacheAble { get; set; }

        public int ExpiryInMinutes { get; set; }

        public CallType CallType { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(this.QueryString)}: {this.QueryString}, {nameof(this.CacheAble)}: {this.CacheAble}, {nameof(this.ExpiryInMinutes)}: {this.ExpiryInMinutes}, {nameof(this.CallType)}: {this.CallType}";
        }
    }
}