using System.Linq;

namespace ProxyMapper.Core
{
    public class DefaultKey
    {
        private const int NoParamKey = 0;
        private const int NullParamKey = 53;

        private readonly object[] _arguments;

        private readonly string _className;

        private readonly string _methodName;

        public DefaultKey(object[] arguments, string methodName, string className)
        {
            this._arguments = arguments;
            this._methodName = methodName;
            this._className = className;
        }

        public override bool Equals(object obj)
        {
            DefaultKey secondKey = obj as DefaultKey;
            return secondKey != null &&
                   (this == obj ||
                    this._methodName.Equals(secondKey._methodName) && this._className.Equals(secondKey._className) &&
                    this._arguments.SequenceEqual(secondKey._arguments));
        }

        public override int GetHashCode()
        {
            int hashCode = 17;
            hashCode = 31 * hashCode + this._className.GetHashCode();
            hashCode = 31 * hashCode + this._methodName.GetHashCode();
            if (this._arguments == null || this._arguments.Length == 0)
            {
                return 31 * hashCode + NoParamKey;
            }
            return this._arguments.Aggregate(hashCode,
                (current, arguemnt) => 31 * current + (arguemnt?.GetHashCode() ?? NullParamKey));
        }
    }
}