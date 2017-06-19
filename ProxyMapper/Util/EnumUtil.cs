namespace ProxyMapper.Util
{
    using System;

    internal class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            try
            {
                return (T) Enum.Parse(typeof(T), value, true);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(
                    "Parsing failed for Enum type " + typeof(T) + " with value - " + value, e);
            }
        }
    }
}