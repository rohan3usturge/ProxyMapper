namespace ProxyMapper.Util
{
    using System;

    internal static class AssertExtensions
    {
        public static void AssertNotNull(this object o, string errorMessage)
        {
            if (o == null)
            {
                throw new ArgumentNullException(errorMessage);
            }
        }

        public static void AssertNotNullOrEmpty(this string str, string error)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException(error);
            }
        }
    }
}