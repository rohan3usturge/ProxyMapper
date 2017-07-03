using System;
using ProxyMapper.Enums;

namespace ProxyMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CallInfo : Attribute
    {
        public CallInfo(string queryString)
        {
            this.QueryString = queryString;
            this.CallType = CallType.Procedure;
        }

        public CallInfo(string queryString, CallType callType)
        {
            this.QueryString = queryString;
            this.CallType = callType;
        }

        public string QueryString { get; set; }

        public CallType CallType { get; set; }

        public override string ToString()
        {
            return $"{nameof(this.QueryString)}: {this.QueryString}, {nameof(this.CallType)}: {this.CallType}";
        }
    }
}